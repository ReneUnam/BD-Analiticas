using APPCORE;
using BusinessLogic.Connection;
namespace Producto
{
    class Cat_Producto : EntityClass
    {
        public Cat_Producto()
        {
            this.MDataMapper = new BDConnection().DBOrigen;
        }

        [PrimaryKey(Identity = true)]
        public int? IdProducto { get; set; }
        public string? Nombre { get; set; }
        public int? IdCategoria { get; set; }
        public int? IdLaboratorio { get; set; }
    }

    class Cat_DetalleProducto : EntityClass
    {
        public Cat_DetalleProducto()
        {
            this.MDataMapper = new BDConnection().DBOrigen;
        }

        [PrimaryKey(Identity = true)]
        public int? Detalle_Id { get; set; }
        public int? Detalle_IdProducto { get; set; }
        public string? Detalle_Descripcion { get; set; }
        public int? Detalle_IdUnidadMedida { get; set; }
        public DateTime? Detalle_FechaVencimiento { get; set; }
    }

    class Tbl_ProductoAlmacenado : EntityClass
    {
        public Tbl_ProductoAlmacenado()
        {
            this.MDataMapper = new BDConnection().DBOrigen;
        }

        [PrimaryKey(Identity = true)]
        public int? Almc_Id { get; set; }
        public int? Almc_Detalle_Id { get; set; }
        public decimal? Almc_PrecioVenta { get; set; }
        public decimal? Almc_PrecioCompra { get; set; }
    }

    // public class Categorias
    // {
    //     public int IdCategoria { get; set; }
    //     public string Nombre { get; set; }
    // }

    // public class Laboratorio
    // {
    //     public int IdLaboratorio { get; set; }
    //     public string Nombre { get; set; }
    // }
}