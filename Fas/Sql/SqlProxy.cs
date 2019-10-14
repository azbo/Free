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

            foreach (DbParameter p in pms)
            {
                cmd.Parameters.Add(p);
            }

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
            if (action == "DbData") action = ((DbData)args[0])["model"].ToString();

            SqlConfig sc = new SqlConfig(action);
            string table = sc["Data"]["table"];

            string key = $"{m.Name}.{args[1]}";

            Dictionary<string, string> dict = sc[key];
            string field = sc["Field"][dict["id"]];

            Config conf = Config.Instance;

            string db = sc["Data", "db"] ?? conf["fas.db", "conn"];

            fac = conf.GetDbFactory(db);
            link = conf[$"fas.db.{db}", "link"];

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
            DbData dbData = null;
            if (data is DbData) dbData = (DbData)data;

            string[] fields = field.Split(",");

            DbParameter[] pms = new DbParameter[fields.Length];

            string values = string.Empty;
            for (int i = 0; i < fields.Length; i++)
            {
                string name = $"@{fields[i]}";

                DbParameter pm = fac.CreateParameter();
                pm.ParameterName = name;
                pm.Value = dbData[fields[i]];
                pms[i] = pm;
                
                values += i == fields.Length - 1 ? name : $"{fields[i]},";
            }

            string sql = $"insert into {table}({field}) values({values})";
            return ExecuteNonQuery(sql, pms);
        }
    }
}
