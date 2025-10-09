using APPCORE;
using BusinessLogic.Connection;
namespace Laboratory
{
    class DimLaboratorio : EntityClass
    {
        public DimLaboratorio()
        {
            this.MDataMapper = new BDConnection().DBDestino;
        }

        [PrimaryKey(Identity = false)]
        public int? IdLaboratorio { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
