using APPCORE;
using BusinessLogic.Connection;

namespace Operations.Purchases
{
    public class PurchasesFactQuery : QueryClass
    {
        public PurchasesFactQuery()
        {
            this.MDataMapper = new BDConnection().DBOrigen;
        }

        // Campos aplanados del JOIN (Header + Detail)
        public int IdProveedor { get; set; }
        public int IdUsuario { get; set; }
        public int IdDetalleCompra { get; set; }
        public int IdProducto { get; set; }
        public DateTime FechaCompra { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public DateTime? Updated_At { get; set; } // Fecha del Encabezado (para incremental)

        // Implementación del método Get<T>()
        public override List<PurchasesFactQuery> Get<PurchasesFactQuery>()
        {
            var dt = this.MDataMapper?.GDatos.TraerDatosSQL(GetQuery());

            if (dt != null && dt.Rows.Count > 0)
            {
                return AdapterUtil.ConvertDataTable<PurchasesFactQuery>(dt, this);
            }
            else
            {
                return new List<PurchasesFactQuery>();
            }
        }

        // Implementación del método GetQuery()
        public override string GetQuery()
        {
            // Se asume que 'this.UpdateAt' contiene la fecha de corte
            string lastUpdateDate = this.Updated_At.HasValue
                ? this.Updated_At.Value.ToString("yyyy-MM-dd HH:mm:ss")
                : "1900-01-01 00:00:00";

            return $@"
                SELECT 
                    D.IdDetalleCompra, 
                    D.IdCompra, 
                    H.FechaCompra, 
                    H.IdProveedor,
                    H.IdUsuario, 
                    D.IdProducto,
                    D.Cantidad, 
                    D.Subtotal,
                    H.Total,
                    H.Updated_At
                FROM Compras.Compras H
                INNER JOIN Compras.DetalleCompra D ON H.IdCompra = D.IdCompra
                WHERE H.Updated_At > '{lastUpdateDate}'
                ORDER BY H.Updated_At ASC";
        }
    }
}