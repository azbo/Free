using Fas;
using Fas.Logging;
using Fas.Sql;
using System.Reflection;

namespace Test
{
    class Program
    {
        static ILogger log = Logger.GetLogger<Program>();
        static void Main(string[] args)
        {
            //MySql.Data.MySqlClient.MySqlClientFactory

            var queryProxy = DispatchProxy.Create<ISql, SqlProxy>();
        }
    }
}
