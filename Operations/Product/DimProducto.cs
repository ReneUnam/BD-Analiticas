using APPCORE;
using BusinessLogic.Connection;
namespace Product
{
    class DimProducto : EntityClass
    {
        public DimProducto()
        {
            this.MDataMapper = new BDConnection().DBDestino;
        }

        [PrimaryKey(Identity = false)]
        public int? IdProducto { get; set; }
        public string? Nombre { get; set; }
        public int? IdCategoria { get; set; }
        public int? IdLaboratorio { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
