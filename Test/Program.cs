using Fas;
using Fas.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Xml;
using Test.query;

namespace Test
{
    class Program
    {
        static ILogger log = Logger.GetLogger<Program>();
        static void Main(string[] args)
        {
            Config config = Config.Instance;


            log.Info("111111");

            var queryProxy = DispatchProxy.Create<ISql, SqlProxy>();

            User user = new User()
            {
                UserName = "test1",
                PassWord = "azbo1020",
                Age = 12,
                Sex = true
            };
            //执行接口方法
            int rows = (int)queryProxy.insert(user, "save");

            rows = (int)queryProxy.insert(user, "saveList");
        }
    }
    public class SqlProxy : DispatchProxy
    {
        private static NameValueCollection _conf;
        private static DateTime _dt;
        /// <summary>
        /// 拦截调用
        /// </summary>
        /// <param name="method">所拦截的方法信息</param>
        /// <param name="parameters">所拦截方法被传入的参数指</param>
        /// <returns></returns>
        protected override object Invoke(MethodInfo m, object[] args)
        {
            ParameterInfo[] infos = m.GetParameters();

            string action = infos[0].ParameterType.Name;

            SqlConfig sc = SqlConfig.Instance.Load(action);

            string tableName = sc.Get<string>($"{m.Name}.tableName");

            string key = $"{m.Name}.{args[1]}";

            Dictionary<string, string> dict = sc.Get<Dictionary<string, string>>(key);

            string fieldValue = sc.Get<string>($"{m.Name}.field.{dict["id"]}");

            string sql = "";
            switch (action)
            {
                case "insert":
                    if (dict["value"].IndexOf("insert") == 0)
                        sql = $"insert into {tableName}({fieldValue}) values()";

                    break;
                case "remove":
                    break;
                case "modify":
                    break;
                case "query":
                    break;
                default:
                    break;
            }

            //(string, object, string) result = s(key);

            return 1;
        }

        private void LoadXml(XmlNode node, string key = "")
        {
            key += string.IsNullOrEmpty(key) ? node.Name : $".{node.Name}";

            foreach (XmlNode child in node.ChildNodes)
            {
                foreach (XmlAttribute attr in child.Attributes)
                {
                    if (attr.Name == "id")
                    {
                        _conf[$"{key}.{child.Name}.{attr.Value}"] = child.Value;
                        continue;
                    }
                    _conf[$"{key}.{child.Name}.{attr.Name}"] = attr.Value;
                }
            }
        }
    }
}
