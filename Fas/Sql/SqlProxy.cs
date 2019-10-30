using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using System.Text;

namespace Fas.Sql
{
    public class SqlProxy : DispatchProxy
    {
        private DbProvider _db;
        protected override object Invoke(MethodInfo m, object[] args)
        {
            ParameterInfo[] infos = m.GetParameters();

            string action = infos[0].ParameterType.Name;
            if (action == "DbListData") action = ((DbListData)args[0]).ModelName;
            Config conf = Config.Instance;

            string table = conf[$"{action}.table"];
            string field = conf[$"{action}.field.{conf[$"{action}.{m.Name}.{args[1]}.id"]}"];

            _db = conf.GetDbFactory(conf[$"{action}.db"]);

            var prop = conf[$"{action}.prop"];
            return m.Name switch
            {
                "insert" => BuildInsert(table, prop, field, args[0]),
                "remove" => BuildRemove(table, prop, field, args[0]),
                "update" => BuildModify(table, prop, field, args[0]),
                "select" => BuildQuery(table, prop, field, args[0]),
                _ => ""
            };
        }

        private object BuildQuery(string table, dynamic prop, string field, object v)
        {
            throw new NotImplementedException();
        }

        private object BuildRemove(string table, dynamic prop, string field, object v)
        {
            throw new NotImplementedException();
        }

        private object BuildModify(string table, dynamic prop, string field, object v)
        {
            throw new NotImplementedException();
        }

        private int BuildInsert(string table, dynamic prop, string field, dynamic data)
        {
            StringBuilder sb = new StringBuilder()
                .Append($"insert into {table}({field}) values");

            string[] fields = field.Split(",");

            List<DbParameter> pmList = new List<DbParameter>();

            int m = 0;
            for (int i = 0; i < data.list.Count; i++)
            {
                sb.Append("(");
                for (int j = 0; j < fields.Length; j++)
                {
                    string k = fields[j];
                    string name = $"@{k}{m}";

                    string value = "";

                    if (data.list[i].ContainsKey(k))
                    {
                        dynamic v = data.list[i][k];
                        value = v is string ? v : v.ToString();
                    }

                    pmList.Add(_db.CreateParameter(name, value, prop[k]["type"]));

                    sb.Append(name);
                    if (j != fields.Length - 1) sb.Append(",");
                    m++;
                }
                sb.Append(")");
                if (i != data.list.Count - 1) sb.Append(",");
            }
            return _db.ExecuteNonQuery(sb.ToString(), pmList.ToArray());
        }
    }
}
