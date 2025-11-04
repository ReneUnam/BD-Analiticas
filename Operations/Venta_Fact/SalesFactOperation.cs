using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using APPCORE;
using BusinessLogic.Connection;
using Producto;
using Operations.Time;
using Customer;
using Users;

namespace Operations.Sales
{
    public class SalesFactOperation
    {
        public void Execute()
        {
            Console.WriteLine("=== Iniciando ETL de Hechos de Venta ===");

            // Obtener fecha de la última carga
            var lastUpdate = DateOLAPOperation.GetLastUpdatedate();

            // EXTRAER datos del OLTP (solo los nuevos desde la última carga)
            var query = new SalesFactQuery { Updated_At = lastUpdate };
            var sourceRows = query.Get<SalesFactQuery>() ?? new List<SalesFactQuery>();

            if (sourceRows.Count == 0)
            {
                Console.WriteLine("No hay nuevas ventas para cargar.");
                DateOLAPOperation.UpdateLastUpdateDate(DateTime.Now);
                return;
            }

            // Cargar dimensiones (para los lookups)
            var productos = new ProductoDIM().Get<ProductoDIM>() ?? new List<ProductoDIM>();
            var clientes = new ClienteDIM().Get<ClienteDIM>() ?? new List<ClienteDIM>();
            var usuarios = new UsuarioDIM().Get<UsuarioDIM>() ?? new List<UsuarioDIM>();

            // Convertir a diccionarios para búsquedas rápidas
            var prodLookup = productos
                .Where(p => p.IdProductoOLTP.HasValue)
                .ToDictionary(p => p.IdProductoOLTP!.Value, p => p);

            var cliLookup = clientes
                .Where(c => c.IdCliente.HasValue)
                .ToDictionary(c => c.IdCliente!.Value, c => c);

            var userLookup = usuarios
                .Where(u => u.IdUsuario.HasValue)
                .ToDictionary(u => u.IdUsuario!.Value, u => u);

            // 4️⃣ TRANSFORMAR registros
            var facts = new List<FactVentas>();
            foreach (var s in sourceRows)
            {
                int fechaKey = int.Parse(s.FechaVenta.Date.ToString("yyyyMMdd", CultureInfo.InvariantCulture));

                // Producto_Key: se busca por el Detalle_Id (porque así se cargó en ProductoDIM)
                int? productoKey = prodLookup.ContainsKey(s.IdProductoAlmacenado)
                    ? prodLookup[s.IdProductoAlmacenado].IdProductoOLTP
                    : null;

                if (!productoKey.HasValue)
                {
                    Console.WriteLine($"Producto no encontrado para IdProductoAlmacenado={s.IdProductoAlmacenado}");
                    continue; // puedes optar por continuar o insertar con 0
                }

                // Otros lookups (cliente, usuario)
                int? clienteKey = cliLookup.ContainsKey(s.IdCliente) ? s.IdCliente : null;
                int? vendedorKey = userLookup.ContainsKey(s.IdUsuario) ? s.IdUsuario : null;

                var fact = new FactVentas
                {
                    Fecha_Key = fechaKey,
                    Producto_Key = productoKey ?? 0,
                    Cliente_Key = clienteKey ?? 0,
                    Vendedor_Key = vendedorKey ?? 0,

                    IdDetalleVenta_OLTP = s.IdDetalleVenta,
                    Cantidad = s.Cantidad,
                    Subtotal = s.Subtotal,
                    Descuento = s.Descuento,
                    Total = s.Total
                };

                facts.Add(fact);
            }

            // CARGAR (insertar en el DW)
            foreach (var f in facts)
                f.Save();

            // Actualizar fecha de última carga
            DateOLAPOperation.UpdateLastUpdateDate(DateTime.Now);

            Console.WriteLine($"Cargadas {facts.Count} filas nuevas en FactVentas.");
        }
    }
}
