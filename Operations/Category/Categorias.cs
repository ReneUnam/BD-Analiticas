using APPCORE;
using BusinessLogic.Connection;
namespace Category
{
    class Categorias : EntityClass
    {
        public Categorias()
        {
            this.MDataMapper = new BDConnection().DBOrigen;
        }
        [PrimaryKey(Identity = true)]
        public int? IdCategoria { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
    }
}