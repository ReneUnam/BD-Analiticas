using System;
using APPCORE;
using BusinessLogic.Connection;

namespace Operations.Sales
{
    public class VentasSource : EntityClass
    {
        public VentasSource()
        {
            this.MDataMapper = new BDConnection().DBOrigen; // source DB
        }

        public int? IdVenta { get; set; }
        public DateTime? Fecha { get; set; }
        public int? IdCliente { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdProducto { get; set; }
        public int? Cantidad { get; set; }
        public decimal? PrecioUnitario { get; set; }
        public decimal? Descuento { get; set; }
        public decimal? Total { get; set; }
    }
}
