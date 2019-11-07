using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Fas.Sql
{
    public class Sql : ISql, IAdo
    {
        private Config cf = Config.Instance;

        private DbProvider _db;

        private string name;

        private string field = "all";

        public Sql()
        {

        }

        public Sql(string name)
        {
            this.name = name;

            _db = cf.GetDbFactory(cf[$"{name}.db"]);
        }

        public int Delete()
        {
            throw new NotImplementedException();
        }

        public ISql Field(string field)
        {
            if (string.IsNullOrEmpty(field) && string.IsNullOrEmpty(this.field)) throw new Exception("属性字段不能为空");

            this.field = cf[$"{name}.field.{field}"];

            return this;
        }

        public T Get<T>()
        {
            throw new NotImplementedException();
        }

        public ISql GroupBy(string id)
        {
            throw new NotImplementedException();
        }

        public ISql Having(string id)
        {
            throw new NotImplementedException();
        }

        public ISql Inner(string id)
        {
            throw new NotImplementedException();
        }

        public int Insert(List<Hashtable> datas)
        {
            string table = cf[$"{name}.table"];
            var prop = cf[$"{name}.prop"];

            string fields = "", values = "";
            List<DbParameter> pmList = new List<DbParameter>();

            for (int i = 0; i < datas.Count; i++)
            {
                values += "(";
                Hashtable data = datas[i];
                foreach (string k in data.Keys)
                {
                    if (!prop.ContainsKey(k)) continue;

                    if (i == 0) fields += $"{k},";

                    string name = $"@{k}{i}";

                    dynamic v = data[k];
                    string value = v is string ? v : v.ToString();

                    pmList.Add(_db.CreateParameter(name, value, prop[k]["type"]));

                    values += $"{name},";
                }

                values = values.TrimEnd(',') + "),";
            }

            fields = fields.TrimEnd(',');

            values = values.TrimEnd(',');

            string sql = "insert into {0} ({1}) values {2}";
            return _db.ExecuteNonQuery(string.Format(sql, table, fields, values), pmList);
        }

        public ISql Limit(string id)
        {
            throw new NotImplementedException();
        }

        public ISql OrderBy(string id)
        {
            throw new NotImplementedException();
        }

        public object Select()
        {
            throw new NotImplementedException();
        }

        public int Update()
        {
            throw new NotImplementedException();
        }

        public ISql Where(string id)
        {
            throw new NotImplementedException();
        }
    }
}
