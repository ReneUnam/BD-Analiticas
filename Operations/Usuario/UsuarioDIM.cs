using APPCORE;
using BusinessLogic.Connection;
namespace Users
{
    class UsuarioDIM : EntityClass
    {
        public UsuarioDIM()
        {
            this.MDataMapper = new BDConnection().DBDestino;
        }
        [PrimaryKey(Identity = false)]
        public int? IdUsuario { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? NombreUsuario { get; set; }
    }
}
