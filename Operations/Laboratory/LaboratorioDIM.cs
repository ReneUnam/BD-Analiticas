using APPCORE;
using BusinessLogic.Connection;
namespace Laboratory
{
    class LaboratorioDIM : EntityClass
    {
        public LaboratorioDIM()
        {
            this.MDataMapper = new BDConnection().DBDestino;
        }

        [PrimaryKey(Identity = false)]
        public int? IdLaboratorio { get; set; }
        public string? Nombre { get; set; }
    }
}