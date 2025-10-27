using APPCORE;
using BusinessLogic.Connection;
namespace Users
{
    class Usuarios : EntityClass
    {
        public Usuarios()
        {
            this.MDataMapper = new BDConnection().DBOrigen;
        }
        [PrimaryKey(Identity = false)]
        public int? IdUsuario { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? NombreUsuario { get; set; }
    }
}