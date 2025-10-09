using APPCORE;
using BusinessLogic.Connection;
namespace User
{
    class Usuario : EntityClass
    {
        public Usuario()
        {
            this.MDataMapper = new BDConnection().DBOrigen;
        }

        [PrimaryKey(Identity = true)]
        public int? IdUsuario { get; set; }
        public string? NombreUsuario { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public int? IdRol { get; set; }
        public bool? Estado { get; set; }
    }
}
