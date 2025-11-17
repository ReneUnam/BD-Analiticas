using Producto;
using Users;
using Supplier;
using System.Globalization;
using APPCORE;

namespace Operations.Purchases
{
    public class PurchasesFactOperation
    {
        public void Execute()
        {
            var startTime = DateTime.Now;
            Console.WriteLine("Iniciando operación de hechos de compras...");

            var lastUpdateDate = HistoricDateOLAPOperation.GetLastUpdatedate();

            var query = new PurchasesFactQuery { Updated_At = lastUpdateDate };
            var sourceRows = query.Get<PurchasesFactQuery>() ?? new List<PurchasesFactQuery>();

            if (sourceRows.Count == 0)
            {
                Console.WriteLine("No hay nuevas compras para cargar.");
                HistoricDateOLAPOperation.UpdateLastUpdateDate(startTime, DateTime.Now, 0);
                return;
            }

            var productos = new ProductoDIM().Get<ProductoDIM>() ?? new List<ProductoDIM>();
            var usuarios = new UsuarioDIM().Get<UsuarioDIM>() ?? new List<UsuarioDIM>();
            var proveedores = new ProveedoresDIM().Get<ProveedoresDIM>() ?? new List<ProveedoresDIM>();

            var prodLookup = productos
                .Where(p => p.IdProductoOLTP.HasValue)
                .ToDictionary(p => p.IdProductoOLTP!.Value, p => p);

            var userLookup = usuarios
                .Where(u => u.IdUsuario.HasValue)
                .ToDictionary(u => u.IdUsuario!.Value, u => u);

            var provLookup = proveedores
                .Where(p => p.IdProveedor.HasValue)
                .ToDictionary(p => p.IdProveedor!.Value, p => p);

            var facts = new List<FactCompras>();
            foreach (var s in sourceRows)
            {
                int fechaKey = int.Parse(s.FechaCompra.Date.ToString("yyyyMMdd", CultureInfo.InvariantCulture));

                int? productoKey = prodLookup.ContainsKey(s.IdProducto)
                    ? prodLookup[s.IdProducto].IdProductoOLTP
                    : null;

                if (!productoKey.HasValue)
                {
                    Console.WriteLine($"Producto no encontrado para IdProducto={s.IdProducto}");
                    continue;
                }

                int? usuarioKey = userLookup.ContainsKey(s.IdUsuario)
                    ? userLookup[s.IdUsuario].IdUsuario
                    : null;

                if (!usuarioKey.HasValue)
                {
                    Console.WriteLine($"Usuario no encontrado para IdUsuario={s.IdUsuario}");
                    continue;
                }

                int? proveedorKey = provLookup.ContainsKey(s.IdProveedor)
                    ? provLookup[s.IdProveedor].IdProveedor
                    : null;

                if (!proveedorKey.HasValue)
                {
                    Console.WriteLine($"Proveedor no encontrado para IdProveedor={s.IdProveedor}");
                    continue;
                }

                var fact = new FactCompras
                {
                    Fecha_Key = fechaKey,
                    Producto_Key = productoKey.Value,
                    Usuario_Key = usuarioKey.Value,
                    Proveedor_Key = proveedorKey.Value,

                    IdDetalleCompra_OLTP = s.IdDetalleCompra,
                    Cantidad = s.Cantidad,
                    Subtotal = s.Subtotal,
                    Total = s.Total
                };
                facts.Add(fact);
            }

            // CARGAR (insertar o actualizar en el DW)
            foreach (var f in facts)
            {
                // *** MODIFICACIÓN CLAVE: Buscamos por la CLAVE COMPUESTA ***
                var existingFact = new FactCompras().Find<FactCompras>(
                    FilterData.And(
                        FilterData.Equal("Fecha_Key", f.Fecha_Key),
                        FilterData.Equal("Producto_Key", f.Producto_Key),
                        FilterData.Equal("Usuario_Key", f.Usuario_Key),
                        FilterData.Equal("Proveedor_Key", f.Proveedor_Key),
                        FilterData.Equal("IdDetalleCompra_OLTP", f.IdDetalleCompra_OLTP) // Asumo que esta es la 5ta clave
                    )
                );

                if (existingFact != null)
                {
                    // El registro existe, por lo tanto, actualizamos (Update).

                    // No es necesario actualizar las claves de dimensión si no cambian,
                    // solo las medidas (metrics/facts) y atributos.
                    existingFact.Cantidad = f.Cantidad;
                    existingFact.Subtotal = f.Subtotal;
                    existingFact.Total = f.Total;

                    // Llamamos a Update sobre la instancia existente.
                    existingFact.Update();
                }
                else
                {
                    // El registro es nuevo, por lo tanto, insertamos (Insert).
                    f.Save();
                }
            }

            // Actualizar fecha de última carga
            Console.WriteLine($"Cargadas {facts.Count} filas nuevas en FactCompras.");
            HistoricDateOLAPOperation.UpdateLastUpdateDate(startTime, DateTime.Now, facts.Count);
        }
    }
}