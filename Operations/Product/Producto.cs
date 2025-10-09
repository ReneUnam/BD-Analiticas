using APPCORE;
using BusinessLogic.Connection;
namespace Product
{
    class Producto : EntityClass
    {
        public Producto()
        {
            this.MDataMapper = new BDConnection().DBOrigen;
        }

        [PrimaryKey(Identity = true)]
        public int? IdProducto { get; set; }
        public string? Nombre { get; set; }
        public int? IdCategoria { get; set; }
        public int? IdLaboratorio { get; set; }
        public bool? Estado { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
