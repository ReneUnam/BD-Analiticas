using APPCORE;
using BusinessLogic.Connection;

namespace Supplier
{
    class ProveedoresDIM : EntityClass
    {
        public ProveedoresDIM()
        {
            this.MDataMapper = new BDConnection().DBDestino;
        }

        [PrimaryKey(Identity = false)]
        public int? IdProveedor { get; set; }
        public string? Nombre { get; set; }
    }
}