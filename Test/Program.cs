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

            DbData novel = new DbData("Novel") {
                {"category","1"},
                {"title","1"},
                {"author","1"},
                {"pic","1"},
                {"content","1"},
                {"tag","1"},
                {"up","1"},
                {"down","1"},
                {"hits","1"},
                {"rating","1"},
                {"rating_count","1"},
                {"serialize","1"},
                {"favorites","1"},
                {"position","1"},
                {"template","1"},
                {"link","1"},
                {"create_time","1"},
                {"update_time","1"},
                {"reurl","1"},
                {"status","1"},
                {"hits_day","1"},
                {"hits_week","1"},
                {"hits_month","1"},
                {"hits_time","1"},
                {"word","1"},
                {"recommend","1"},
            };
            //执行接口方法
            int rows = (int)queryProxy.Insert(novel, "Save");

            rows = (int)queryProxy.Insert(novel, "SaveList");
        }
    }
}
