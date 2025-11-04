using APPCORE;
using BusinessLogic.Connection;
namespace Customer
{
    class Clientes : EntityClass
    {
        public Clientes()
        {
            this.MDataMapper = new BDConnection().DBOrigen;
        }
        [PrimaryKey(Identity = true)]
        public int? IdCliente { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
    }
}