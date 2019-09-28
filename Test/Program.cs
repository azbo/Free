using Fas.Engine;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Xml;
using Test.query;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            TemplateEngine engine = new TemplateEngine();
            engine.BeginInit();

            Dictionary<string, object> di = new Dictionary<string, object>();
            di["test"] = "122121";
            using (StringWriter writer = new StringWriter())
            {
                engine.Process(di, "listHw", writer,"listhw");
            }

            //object obj = Activator.CreateInstance(Type.GetType("MySql.Data.MySqlClient.MySqlClientFactory"));

            var queryProxy = DispatchProxy.Create<HomeWorkMapper, QueryProxy>();

            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict["id"] = "a";
            dict["age"] = "12";

            list.Add(dict);
            //执行接口方法
            int rows = queryProxy.Insert(list);

            TestInstance testInstance = new TestInstance();
            testInstance.TestMethod();
        }

        private static NameValueCollection _conf = new NameValueCollection();

        static void LoadXml(XmlNode node, string path = "")
        {
            path += string.IsNullOrEmpty(path) ? node.Name : $".{node.Name}";

            foreach (XmlAttribute attr in node.Attributes)
            {
                _conf[$"{path}.{attr.Name}"] = attr.Value;
            }

            foreach (XmlNode child in node.ChildNodes)
            {
                LoadXml(child, path);
            }
        }

        [Query]
        public class TestInstance : ContextBoundObject
        {
            public void TestMethod()
            {
                Console.WriteLine("MethodAction……");
            }
        }
    }

    public interface Homework
    {
        //这个方法会被代理类实现
        void Insert(string str);
    }

    public class QueryProxy : DispatchProxy
    {
        /// <summary>
        /// 拦截调用
        /// </summary>
        /// <param name="method">所拦截的方法信息</param>
        /// <param name="parameters">所拦截方法被传入的参数指</param>
        /// <returns></returns>
        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            Console.WriteLine(args[0]);
            return 1;
        }
    }
}
