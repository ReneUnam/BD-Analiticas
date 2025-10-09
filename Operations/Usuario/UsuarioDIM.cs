using APPCORE;
using BusinessLogic.Connection;
namespace Usuarios
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
        public bool? NombreUsuario { get; set; }
        public bool? UsuarioSalt { get; set; }
        public bool? Contraseña { get; set; }
        public bool? IdRol { get; set; }
        public bool? Estado { get; set; }
     
    }
}
