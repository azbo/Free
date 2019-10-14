using Fas.Logging;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Fas
{
    public class SqlConfig
    {
        private static ILogger log = Logger.GetLogger<SqlConfig>();
        private static string _m;
        private static readonly MemoryCache cache = new MemoryCache(new MemoryCacheOptions());

        public virtual Dictionary<string, string> this[string key]
        {
            get
            {
                var dict = cache.Get<Dictionary<string, Dictionary<string, string>>>(_m);
                if (dict.ContainsKey(key))  return dict[key];
                return new Dictionary<string, string>();
            }
        }

        public virtual string this[string k1, string k2]
        {
            get
            {
                if (this[k1].ContainsKey(k2)) return this[k1][k2];
                return null;
            }
        }

        private SqlConfig()
        {

        }

        public SqlConfig(string m)
        {
            _m = m;
            try
            {
                string sqlXml = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", $"{m}.xml");
                if (!File.Exists(sqlXml))
                {
                    throw new Exception("未查到相关文件");
                }

                FileInfo fi = new FileInfo(sqlXml);

                var dict = cache.Get<Dictionary<string, Dictionary<string, string>>>(m) ?? new Dictionary<string, Dictionary<string, string>>() { { "file", new Dictionary<string, string>() { { "update", "" } } } };

                if (dict["file"]["update"] == fi.LastWriteTime.ToString()) return;

                dict["file"]["update"] = fi.LastWriteTime.ToString();

                XmlDocument x = new XmlDocument();
                x.Load(fi.OpenText());

                XmlNode root = x.DocumentElement;

                var dict2 = new Dictionary<string, string>();
                foreach (XmlAttribute attr in root.Attributes)
                {
                    dict2[$"{attr.Name}"] = attr.Value;
                }
                dict[root.Name] = dict2;

                foreach (XmlNode node in root.ChildNodes)
                {
                    dict2 = new Dictionary<string, string>();
                    foreach (XmlAttribute attr in node.Attributes)
                    {
                        dict2[attr.Name] = attr.Value;
                    }
                    dict[node.Name] = dict2;

                    foreach (XmlNode node2 in node.ChildNodes)
                    {
                        dict2 = new Dictionary<string, string>();
                        foreach (XmlAttribute attr in node2.Attributes)
                        {
                            dict2[attr.Name] = attr.Value;
                        }

                        dict2["text"] = node2.InnerText;

                        dict[$"{node.Name}.{node2.Name}"] = dict2;
                    }
                }

                cache.Set(m, dict);
            }
            catch (Exception e)
            {
                log.Error(e);
            }

        }
    }
}
