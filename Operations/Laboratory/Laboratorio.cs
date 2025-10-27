using APPCORE;
using BusinessLogic.Connection;
namespace Laboratory
{
    class Laboratorio : EntityClass
    {
        public Laboratorio()
        {
            this.MDataMapper = new BDConnection().DBOrigen;
        }

        [PrimaryKey(Identity = true)]
        public int? IdLaboratorio { get; set; }
        public string? Nombre { get; set; }
    }
}