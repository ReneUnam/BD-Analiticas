using APPCORE;
using BusinessLogic.Connection;
namespace Units
{
    class UnidadesDIM : EntityClass
    {
        public UnidadesDIM()
        {
            this.MDataMapper = new BDConnection().DBDestino;
        }

        [PrimaryKey(Identity = false)]
        public int IdUnidad { get; set; }
        public string Nombre { get; set; }
        public string Abreviatura { get; set; }
    }
}