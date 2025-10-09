using System;
using APPCORE;
using BusinessLogic.Connection;

namespace Time
{
    public class DimTiempo : EntityClass
    {
        public DimTiempo()
        {
            // Dimensiones residen en el data mapper destino (DW)
            this.MDataMapper = new BDConnection().DBDestino;
        }

        [PrimaryKey(Identity = true)]
        public int? IdTiempo { get; set; }
        public DateTime Fecha { get; set; }
        public int Año { get; set; }
        public int Mes { get; set; }
        public int Dia { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
