using Producto;
using Users;
using Supplier;
using System.Globalization;

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

            // CARGAR (insertar en el DW)
            foreach (var f in facts)
                f.Save();

            // Actualizar fecha de última carga
            HistoricDateOLAPOperation.UpdateLastUpdateDate(startTime, DateTime.Now, facts.Count);

            Console.WriteLine($"Cargadas {facts.Count} filas nuevas en FactCompras.");
        }
    }
}