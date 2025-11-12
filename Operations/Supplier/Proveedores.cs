using APPCORE;
using BusinessLogic.Connection;

namespace Supplier
{
    class Proveedores : EntityClass
    {
        public Proveedores()
        {
            this.MDataMapper = new BDConnection().DBOrigen;
        }
        [PrimaryKey(Identity = true)]
        public int? IdProveedor { get; set; }
        public string? Nombre { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
    }
}