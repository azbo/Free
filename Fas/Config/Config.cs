using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml;

namespace Fas
{
    public class Config
    {
        private readonly Dictionary<string, Dictionary<string, string>> _conf = new Dictionary<string, Dictionary<string, string>>();
        private readonly Dictionary<string, string> _env = new Dictionary<string, string>();

        private static readonly MemoryCache cache = new MemoryCache(new MemoryCacheOptions());

        public virtual Dictionary<string, string> this[string key]
        {
            get
            {
                return _conf[key];
            }
        }

        public virtual string this[string k1, string k2]
        {
            get
            {
                if (_conf[k1].ContainsKey(k2)) return _conf[k1][k2];
                return null;
            }
        }

        public static Config Instance { get; } = new Config();

        static Config()
        {

        }

        private Config()
        {
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

            XmlNode doc = x.DocumentElement;

            var dict = new Dictionary<string, string>();
            foreach (XmlAttribute attr in doc.Attributes)
            {
                dict[attr.Name] = attr.Value;
            }

            _conf["fas"] = dict;

            LoadLog(doc.SelectSingleNode("log"));

            LoadDb(doc.SelectSingleNode("db"));
        }

        private void LoadLog(XmlNode node)
        {
            if (node == null || node.Attributes.Count == 0)
            {
                _conf["fas"]["logPath"] = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs", $"{DateTime.Now.ToString("yyyyMMdd")}.log");
                _conf["fas"]["logMsg"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " {0} {1} {2}";
            }
            else
            {
                var dict = new Dictionary<string, string>();
                foreach (XmlAttribute attr in node.Attributes)
                {
                    dict[attr.Name] = attr.Value;
                }
                _conf[$"{node.Name}"] = dict;

                string pattern = @"\$\{([@:\w-\s]+)\}";
                string logPath = _conf["log"]["file"].Replace('_', Path.DirectorySeparatorChar);

                string logMsg = Regex.Replace(_conf["log"]["msg"], pattern, GetPath);
                string dir = this["fas", "dir"];
                if (string.IsNullOrEmpty(dir))
                    logPath = logPath.Replace("${dir}", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs"));

                logPath = Regex.Replace(logPath, pattern, GetPath);

                _conf["fas"]["logPath"] = logPath;
                _conf["fas"]["logMsg"] = logMsg;
            }

            string path = Path.GetDirectoryName(_conf["fas"]["logPath"]);
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        }

        private void LoadDb(XmlNode node)
        {
            if (node == null || node.Attributes.Count == 0) return;

            var dict = new Dictionary<string, string>();
            foreach (XmlAttribute attr in node.Attributes)
            {
                dict[attr.Name] = attr.Value;
            }

            _conf[$"{node.Name}"] = dict;

            foreach (XmlNode node2 in node.ChildNodes)
            {
                string[] providers = node2.Attributes["provider"].Value.Split(",");
                if (providers.Length != 2) throw new Exception("数据库驱动配置错误");
                dict = new Dictionary<string, string>();
                foreach (XmlAttribute attr in node2.Attributes)
                {
                    dict[attr.Name] = attr.Value;
                }
                _conf[$"{node.Name}.{node2.Name}"] = dict;

                cache.Set($"{node.Name}.{node2.Name}.provider", Assembly.Load(providers[0]).GetType(providers[1]).GetField("Instance").GetValue(null));
            }
        }

        private string GetPath(Match match)
        {
            string value = match.Groups[1].Value;
            string[] formats = value.Split("@");
            if (formats.Length == 2) return DateTime.Now.ToString(formats[1]);
            return this["fas", value] ?? this["fas.log", value] ?? match.Groups[0].Value;
        }

        public DbProviderFactory GetDbFactory(string key)
        {
            return cache.Get<DbProviderFactory>($"db.{key}.provider");
        }
    }
}
