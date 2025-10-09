using APPCORE;
using BusinessLogic.Connection;
namespace Client
{
    class Cliente : EntityClass
    {
        public Cliente()
        {
            this.MDataMapper = new BDConnection().DBOrigen;
        }

        [PrimaryKey(Identity = true)]
        public int? IdCliente { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Telefono { get; set; }
        public bool? Estado { get; set; }
    }
}
