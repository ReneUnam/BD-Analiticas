using System;
using APPCORE;
using BusinessLogic.Connection;

namespace Operations.Connections.Purchases
{
    public class FactCompras : EntityClass
    {
        public FactCompras()
        {
            this.MDataMapper = new BDConnection().DBDestino;
        }

        [PrimaryKey(Identity = false)]
        public int? IdCompra { get; set; }
        public int? IdTiempo { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdProducto { get; set; }
        public int? Cantidad { get; set; }
        public decimal? PrecioUnitario { get; set; }
        public decimal? Total { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
