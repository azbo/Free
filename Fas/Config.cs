using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Xml;

namespace Fas.Config
{
    public class Config
    {
        private NameValueCollection _conf;
        static Config()
        {

        }

        private Config()
        {
            string confPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "fas.conf");
            if (!File.Exists(confPath)) return;

            XmlDocument x = new XmlDocument();
            x.Load(confPath);

            XmlNode root = x.DocumentElement;

            XmlNode node = root.SelectSingleNode("databases");
            if (node == null || node.ChildNodes.Count == 0) return;
            foreach (XmlNode item in node.ChildNodes)
            {
                if (item.Name == "add")
                {
                    string name = item.Attributes["name"]?.Value;
                    if (string.IsNullOrEmpty(name)) continue;

                    string connectionString = item.Attributes["connectionString"]?.Value;
                    if (string.IsNullOrEmpty(connectionString)) continue;

                    string provider = item.Attributes["providerName"]?.Value;
                    if (string.IsNullOrEmpty(provider)) continue;

                    _conf["databases.name"] = name;

                    _conf["databases.provider"] = provider;

                    _conf["databases.connectionString"] = connectionString;
                }
            }
            /*
            string url = Environment.GetEnvironmentVariable("fas_conf_url");
            if (string.IsNullOrEmpty(url))
            {
                loadXml(confPath);
                return;
            }
            string result = new HttpClient().GetAsync(url).Result.Content.ReadAsStringAsync().Result;

            List<Dictionary<string, string>> list = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(result);

            string text = File.ReadAllText(confPath);
            foreach (var item in list)
            {
                //text = text.Replace(,"");
            }

    */
        }

        public static Config Instance { get; } = new Config();

        public string GetConfig(string key)
        {
            return _conf[key];
        }
    }
}
