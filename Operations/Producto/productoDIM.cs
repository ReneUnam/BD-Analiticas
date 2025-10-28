using APPCORE;
using BusinessLogic.Connection;
namespace Producto
{
    class ProductoDIM : EntityClass
    {
        public ProductoDIM()
        {
            this.MDataMapper = new BDConnection().DBDestino;
        }

        [PrimaryKey(Identity = false)]
        public int? IdProductoOLTP { get; set; }
        public string? NombreProducto { get; set; }
        public string? NombreCategoria { get; set; }
        public string? NombreLaboratorio { get; set; }
        public string? NombreUnidad { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public decimal? PrecioVenta { get; set; }
        public decimal? PrecioCompra { get; set; }
        public DateTime? FechaCargaDW { get; set; }
    }
}
