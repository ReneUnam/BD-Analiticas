using APPCORE;
using BusinessLogic.Connection;
using System;
using System.Collections.Generic;
using System.Data;

namespace Operations.Sales
{
    public class SalesFactQuery : QueryClass
    {
        public SalesFactQuery()
        {
            this.MDataMapper = new BDConnection().DBOrigen;
        }

        // Campos aplanados del JOIN (Header + Detail)
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }
        public int IdDetalleVenta { get; set; }
        public int IdProductoAlmacenado { get; set; }
        public DateTime FechaVenta { get; set; }
        public int Cantidad { get; set; }
        public decimal Descuento { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public DateTime? Updated_At { get; set; } // Fecha del Encabezado (para incremental)

        // Implementación del método Get<T>()
        public override List<SalesFactQuery> Get<SalesFactQuery>()
        {
            var dt = this.MDataMapper?.GDatos.TraerDatosSQL(GetQuery());

            if (dt != null && dt.Rows.Count > 0)
            {
                return AdapterUtil.ConvertDataTable<SalesFactQuery>(dt, this);
            }
            else
            {
                return new List<SalesFactQuery>();
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
                    D.IdDetalleVenta, 
                    D.IdVenta, 
                    H.FechaVenta, 
                    H.IdCliente,
                    H.IdUsuario, 
                    D.IdProductoAlmacenado,
                    D.Cantidad, 
                    D.Descuento, 
                    D.Subtotal, 
                    D.Total,
                    H.Updated_At
                FROM 
                    ventas.ventas H
                INNER JOIN 
                    ventas.detalleventa D ON H.IdVenta = D.IdVenta
                WHERE 
                    H.Updated_At > '{lastUpdateDate}';
            ";
        }
    }
}
