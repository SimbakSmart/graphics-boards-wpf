

namespace Infraestructure.Data
{
    public class DBContext
    {
        private static string _con=
        @"Driver={SQL Server};Server=128.17.50.183;Database=EpicorITSMApplication;Uid=Indicador;Pwd=50p0rte2013;";

        public static string GetConnection { get { return _con; } }
    }
}
