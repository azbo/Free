using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Fas
{
    public class SqlConfig
    {
        private static readonly MemoryCache cache = new MemoryCache(new MemoryCacheOptions());
        public static SqlConfig Instance { get; } = new SqlConfig();

        static SqlConfig()
        {

        }

        private SqlConfig()
        {

        }

        public SqlConfig Load(string m)
        {
            string sqlXml = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", $"{m}.xml");
            if (!File.Exists(sqlXml))
            {
                throw new Exception("未查到相关文件");
            }

            FileInfo fi = new FileInfo(sqlXml);

            string timeKey = $"{m}.lastTime";

            if (cache.Get<DateTime>(timeKey) != fi.LastWriteTime)
            {
                cache.Set(timeKey, fi.LastWriteTime);

                XmlDocument x = new XmlDocument();
                x.Load(fi.OpenText());

                XmlNode root = x.DocumentElement;

                foreach (XmlAttribute attr in root.Attributes)
                {
                    cache.Set($"{m}.{attr.Name}", attr.Value);
                }

                foreach (XmlNode node in root.ChildNodes)
                {
                    if (node.Name == "field")
                    {
                        foreach (XmlAttribute attr in node.Attributes)
                        {
                            cache.Set($"{m}.field.{attr.Name}", attr.Value);
                        }
                        continue;
                    }

                    foreach (XmlNode node2 in node.ChildNodes)
                    {
                        Dictionary<string, string> dict = new Dictionary<string, string>();
                        foreach (XmlAttribute attr in node.Attributes)
                        {
                            dict[attr.Name] = attr.Value;
                        }

                        dict["value"] = node2.Value;

                        cache.Set($"{m}.{node.Name}.{node2.Name}", dict);
                    }
                }
            }
            return this;
        }

        public T Get<T>(string key)
        {
            return cache.Get<T>(key);
        }
    }
}
