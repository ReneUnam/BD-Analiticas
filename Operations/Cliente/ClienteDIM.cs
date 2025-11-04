using APPCORE;
using BusinessLogic.Connection;
namespace Customer
{
    class ClienteDIM : EntityClass
    {
        public ClienteDIM()
        {
            this.MDataMapper = new BDConnection().DBDestino;
        }
        [PrimaryKey(Identity = false)]
        public int? IdCliente { get; set; }
        public string? Nombre { get; set; }
    }
}