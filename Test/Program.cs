using Fas.Config;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string result = "[{\"$key1\":\"value1\"},{\"$key2\":\"value2\"},{\"$key3\":\"value3\"}]";

            string confPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "fas.config");
            if (!File.Exists(confPath)) return;

            ListDictionary list = JsonSerializer.Deserialize< ListDictionary>(result);

            XmlDocument x = new XmlDocument();
            x.Load(confPath);

            XmlNode root = x.DocumentElement;

            LoadXml(root);

            string text = File.ReadAllText(confPath);
            foreach (var item in list)
            {
                foreach (var key in item.Keys)
                {
                    text = text.Replace(key, item[key]);
                }
            }

            Console.WriteLine();
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
    }
}
