using APPCORE;
using BusinessLogic.Connection;
namespace Units
{
    class Unidades : EntityClass
    {
        public Unidades()
        {
            this.MDataMapper = new BDConnection().DBOrigen;
        }

        [PrimaryKey(Identity = true)]
        public int? IdUnidad { get; set; }
        public string? Nombre { get; set; }
        public string? Abreviatura { get; set; }
    }
}