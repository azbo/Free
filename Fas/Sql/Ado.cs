using Fas.Logging;
using Microsoft.Extensions.Caching.Memory;
using System.Data;
using System.Data.Common;

namespace Fas.Sql
{
    public class Ado
    {
        private static readonly MemoryCache cache = new MemoryCache(new MemoryCacheOptions());
        private readonly ILogger log = Logger.GetLogger<Ado>();

        private static DbProviderFactory fac;

        public static Ado Instance { get; } = new Ado();

        static Ado()
        {

        }

        private Ado()
        {

        }

        private static DbCommand GetCommand(DbConnection cnt, string sql, params DbParameter[] paramers)
        {
            DbCommand cmd = cnt.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;

            if (paramers == null || paramers.Length == 0)
            {
                return cmd;
            }

            foreach (DbParameter p in paramers)
            {
                cmd.Parameters.Add(p);
            }

            return cmd;
        }

        private static DbConnection GetConnection()
        {
            DbConnection cnt = fac.CreateConnection();
            cnt.ConnectionString = "Server=103.224.250.145;Port=3306;Database=bopay;User=BOPAY;Password=NZ6K8KkxePn2zfL2;";

            if (cnt.State != ConnectionState.Open)
            {
                cnt.Open();
            }

            return cnt;
        }

        public static int ExecuteNonQuery(string sql, DbParameter[] paramers)
        {
            using (DbConnection cnt = GetConnection())
            {
                return GetCommand(cnt, sql, paramers).ExecuteNonQuery();
            }
        }
    }
}
