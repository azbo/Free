using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Xml;

namespace Fas
{
    public class Config
    {
        private readonly NameValueCollection _conf;

        public virtual string this[string key]
        {
            get
            {
                return _conf[key];
            }
        }

        private readonly Dictionary<string, string> _env;

        public static Config Instance { get; } = new Config();

        static Config()
        {

        }

        private Config()
        {
            _conf = new NameValueCollection();
            _env = new Dictionary<string, string>();

            string confPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "fas.xml");
            if (!File.Exists(confPath))
            {
                return;
            }

            XmlDocument x = new XmlDocument();
            x.Load(confPath);
            string url = Environment.GetEnvironmentVariable("FASURL");
            if (!string.IsNullOrEmpty(url))
            {
                string result = new HttpClient().GetAsync(url).Result.Content.ReadAsStringAsync().Result;
                _env = JsonSerializer.Deserialize<Dictionary<string, string>>(result);
            }
            LoadXml(x.DocumentElement);
        }

        private void LoadXml(XmlNode node, string key = "")
        {
            key += string.IsNullOrEmpty(key) ? node.Name : $".{node.Name}";

            foreach (XmlAttribute attr in node.Attributes)
            {
                if (_env.ContainsKey(attr.Value))
                {
                    _conf[$"{key}.{attr.Name}"] = _env[attr.Value];
                    continue;
                }
                _conf[$"{key}.{attr.Name}"] = attr.Value;
            }

            foreach (XmlNode child in node.ChildNodes)
            {
                LoadXml(child, key);
            }
        }
    }
}
