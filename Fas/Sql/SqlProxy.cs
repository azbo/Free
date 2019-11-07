using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using System.Text;

namespace Fas.Sql
{
    /**
        var sql = DispatchProxy.Create<ISql, SqlProxy>();
           sql.Field("sync").ExecuteNonQuery();
        */
    public class SqlProxy : DispatchProxy
    {

        private DbProvider _db;
        protected override object Invoke(MethodInfo m, object[] args)
        {
            ParameterInfo[] infos = m.GetParameters();

            string name = infos[0].ParameterType.Name;
            if (name == "DbListData") name = ((DbListData)args[0]).ModelName;


            return m.GetType();

            /**
            return m.Name switch
            {
                "insert" => BuildInsert(name, args),
                "delete" => BuildDelete(name, args),
                "update" => BuildUpdate(name, args),
                "select" => BuildSelect(name, args),
                _ => ""
            };
    */
        }

        private object BuildSelect(string name, object[] args)
        {
            throw new NotImplementedException();
        }

        private object BuildDelete(string model, object[] args)
        {
            Config conf = Config.Instance;

            dynamic data = args[0];

            _db = conf.GetDbFactory(conf[$"{model}.db"]);

            string table = conf[$"{model}.table"];
            string condition = conf[$"{model}.condition.{args[1]}"];
            var prop = conf[$"{model}.prop"];

            string sql = $"delete from {table} {condition}";

            DbParameter[] pms = new DbParameter[] { };

            return 1;
        }

        private object BuildUpdate(string name, object[] args)
        {
            throw new NotImplementedException();
        }

        private int BuildInsert(string model, object[] args)
        {
            Config conf = Config.Instance;

            dynamic data = args[0];

            _db = conf.GetDbFactory(conf[$"{model}.db"]);

            string table = conf[$"{model}.table"];
            string[] fields = conf[$"{model}.field.{args[1]}"];
            var prop = conf[$"{model}.prop"];

            List<DbParameter> pmList = new List<DbParameter>();

            string sql = "insert into {0} ({1}) values {2}";
            int m = 0;
            string field = "", values = "";
            for (int i = 0; i < data.list.Count; i++)
            {
                values += "(";
                for (int j = 0; j < fields.Length; j++)
                {
                    string k = fields[j];
                    if (m == 0)
                    {
                        field += k;
                        if (j != fields.Length - 1) field += ",";
                    }

                    string name = $"@{k}{m}";

                    string value = "";

                    if (data.list[i].ContainsKey(k))
                    {
                        dynamic v = data.list[i][k];
                        value = v is string ? v : v.ToString();
                    }

                    pmList.Add(_db.CreateParameter(name, value, prop[k]["type"]));

                    values += name;
                    if (j != fields.Length - 1) values += ",";
                    m++;
                }
                values += ")";
                if (i != data.list.Count - 1) values += ",";
            }
            return _db.ExecuteNonQuery(string.Format(sql, table, field, values), pmList);
        }
    }
}
