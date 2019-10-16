using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Text;

namespace Fas.Sql
{
    public class SqlProxy : DispatchProxy
    {
        private DbProviderFactory fac;
        private string link;

        private DbConnection GetConnection()
        {
            DbConnection cnt = fac.CreateConnection();
            cnt.ConnectionString = link;

            if (cnt.State != ConnectionState.Open)
            {
                cnt.Open();
            }

            return cnt;
        }

        private DbCommand GetCommand(DbConnection cnt, string sql, params DbParameter[] pms)
        {
            DbCommand cmd = cnt.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;

            if (pms == null || pms.Length == 0) return cmd;

            cmd.Parameters.AddRange(pms);

            return cmd;
        }

        public int ExecuteNonQuery(string sql, params DbParameter[] pms)
        {
            using (DbConnection cnt = GetConnection())
            {
                return GetCommand(cnt, sql, pms).ExecuteNonQuery();
            }
        }

        protected override object Invoke(MethodInfo m, object[] args)
        {
            ParameterInfo[] infos = m.GetParameters();

            string action = infos[0].ParameterType.Name;
            if (action == "DbListData") action = ((DbListData)args[0]).ModelName;

            SqlConfig sc = new SqlConfig(action);
            string table = sc["Data"]["table"];

            string key = $"{m.Name}.{args[1]}";

            Dictionary<string, string> dict = sc[key];
            string field = sc["Field"][dict["id"]];

            Config conf = Config.Instance;

            string db = sc["Data", "db"] ?? conf["db", "conn"];

            fac = conf.GetDbFactory(db);
            link = conf[$"db.{db}", "link"];

            return m.Name switch
            {
                "Insert" => BuildInsert(table, field, args[0]),
                "Remove" => BuildRemove(table, field, args[0]),
                "Modify" => BuildModify(table, field, args[0]),
                "Query" => BuildQuery(table, field, args[0]),
                _ => ""
            };
        }

        private string BuildQuery(string table, string field, object data)
        {
            throw new NotImplementedException();
        }

        private string BuildModify(string table, string field, object data)
        {
            throw new NotImplementedException();
        }

        private string BuildRemove(string table, string field, object data)
        {
            throw new NotImplementedException();
        }

        private int BuildInsert(string table, string field, object data)
        {
            DbListData dbData = null;
            if (data is DbListData) dbData = (DbListData)data;

            string[] fields = field.Split(",");

            List<DbParameter> pmList = new List<DbParameter>();

            StringBuilder sb = new StringBuilder().Append($"insert into {table}({field}) values");

            int m = 0;
            for (int i = 0; i < dbData.list.Count; i++)
            {
                sb.Append("(");
                for (int j = 0; j < fields.Length; j++)
                {
                    string k = fields[j].Trim(' ');
                    string name = $"@{k}{m}";
                    m++;
                    DbParameter pm = fac.CreateParameter();
                    pm.ParameterName = name;
                    pm.Value = dbData.list[i][k];
                    pmList.Add(pm);

                    sb.Append(name);
                    if (j != fields.Length - 1) sb.Append(",");

                }
                sb.Append(")");
                if (i != dbData.list.Count - 1) sb.Append(",");
            }

            return ExecuteNonQuery(sb.ToString(), pmList.ToArray());
        }
    }
}
