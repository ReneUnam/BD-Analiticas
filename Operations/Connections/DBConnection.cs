using APPCORE;
using APPCORE.BDCore.Abstracts;
namespace BusinessLogic.Connection
{
    public class BDConnection
    {
        public WDataMapper? DBOrigen { get; set; }
        public WDataMapper? DBDestino { get; set; }
        public BDConnection()
        {
            DBOrigen = SqlADOConexion.BuildDataMapper(".", "sa", "Rambito12", "FJOSHUA_NOW_V1");
            DBDestino = SqlADOConexion.BuildDataMapper(".", "sa", "Rambito12", "FJOSHUA_NOW_V1_DESTINOTEST");
            DBDestino?.GDatos.TestConnection();
            DBOrigen?.GDatos.TestConnection();
        }
    }
}