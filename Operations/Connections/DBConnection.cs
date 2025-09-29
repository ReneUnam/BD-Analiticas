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
            DBOrigen = SqlADOConexion.BuildDataMapper(".", "sa", "Rambito12", "FARMACIAJOSHUAV1");
            DBDestino = SqlADOConexion.BuildDataMapper(".", "sa", "Rambito12", "FARMACIAJOSHUA_DESTINO");
            DBDestino?.GDatos.TestConnection();
            DBOrigen?.GDatos.TestConnection();
        }
    }
}