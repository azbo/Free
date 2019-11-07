using Fas.Util;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Fas.Sql
{
    public class DbProvider
    {
        private DbProviderFactory fac;
        private string link;
        public DbProvider(DbProviderFactory fac, string link)
        {
            this.fac = fac;
            this.link = link;
        }

        private DbConnection GetConnection()
        {
            DbConnection cnt = fac.CreateConnection();
            cnt.ConnectionString = link;

            if (cnt.State != ConnectionState.Open) cnt.Open();

            return cnt;
        }

        private DbCommand GetCommand(DbConnection cnt, string sql, List<DbParameter> pmList)
        {
            DbCommand cmd = cnt.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;

            if (pmList == null || pmList.Count == 0) return cmd;

            foreach (DbParameter pm in pmList)
            {
                cmd.Parameters.Add(pm);
            }
            return cmd;
        }

        public DbParameter CreateParameter(string name, string value, string type = "")
        {
            DbParameter pm = fac.CreateParameter();
            pm.ParameterName = name;
            pm.Value = value;
            pm.DbType = type.ToDbType();
            return pm;
        }

        public int ExecuteNonQuery(string sql, List<DbParameter> pmList)
        {
            using (DbConnection cnt = GetConnection())
            {
                return GetCommand(cnt, sql, pmList).ExecuteNonQuery();
            }
        }
    }
}
