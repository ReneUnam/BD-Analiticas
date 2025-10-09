using System;
using APPCORE;
using BusinessLogic.Connection;

namespace Operations.Purchases
{
    public class ComprasSource : EntityClass
    {
        public ComprasSource()
        {
            this.MDataMapper = new BDConnection().DBOrigen; // source DB
        }

        public int? IdCompra { get; set; }
        public DateTime? Fecha { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdProducto { get; set; }
        public int? Cantidad { get; set; }
        public decimal? PrecioUnitario { get; set; }
        public decimal? Total { get; set; }
    }
}
