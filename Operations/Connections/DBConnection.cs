using APPCORE;
using APPCORE.BDCore.Abstracts;
namespace BusinessLogic.Connection
{
    public class BDConnection
    {
        public WDataMapper? BDOrigen { get; set; }
        public WDataMapper? BDDestino { get; set; }
        public BDConnection()
        {
            BDOrigen = SqlADOConexion.BuildDataMapper(".", "sa", "Rambito12", "BdOrigen");
            BDDestino = SqlADOConexion.BuildDataMapper(".", "sa", "Rambito12", "BdDestino");
            BDDestino?.GDatos.TestConnection();
            BDOrigen?.GDatos.TestConnection();
        }
    }
}