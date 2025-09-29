using APPCORE;
using BusinessLogic.Connection;
namespace Category
{
    class CategoriasDIM : EntityClass
    {
        public CategoriasDIM()
        {
            this.MDataMapper = new BDConnection().DBDestino;
        }
        [PrimaryKey(Identity = false)]
        public int? IdCategoria { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool? Estado { get; set; }
    }
}