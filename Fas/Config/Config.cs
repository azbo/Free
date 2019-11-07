using Fas.Sql;
using Fas.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Fas
{
    public class Config
    {
        private readonly Hashtable _ht = new Hashtable();
        private readonly XmlDocument _doc = new XmlDocument();

        private readonly Dictionary<string, DbProvider> _dbs = new Dictionary<string, DbProvider>();

        public static Config Instance { get; } = new Config();

        public virtual dynamic this[string key]
        {
            get
            {
                dynamic d = _ht;

                string[] keys = key.Split(".");
                for (int i = 0; i < keys.Length; i++)
                {
                    d = d[keys[i]];
                }
                return d;
            }
        }

        static Config()
        {

        }

        private Config()
        {
            string app = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");
            if (!Directory.Exists(app)) return;

            string[] files = Directory.GetFiles(app);
            foreach (string file in files)
            {
                LoadAppData(file);
            }

            FileSystemWatcher watch = new FileSystemWatcher(app)
            {
                EnableRaisingEvents = true,
            };

            watch.Changed += (object sender, FileSystemEventArgs e) => LoadAppData(e.FullPath);
        }

        private void LoadAppData(string path)
        {
            _doc.Load(path);

            XmlNode root = _doc.DocumentElement;
            var ht = root.ToHashtable();

            LoadXml(root, ht);
            _ht[root.Name] = ht;
        }

        private void LoadXml(XmlNode root, Hashtable ht)
        {
            foreach (XmlNode node in root.ChildNodes)
            {
                if (root.Name == "db")
                {
                    string[] providers = node.Attributes["provider"].Value.Split(",");
                    _dbs[node.Name] = new DbProvider(
                        (DbProviderFactory)Assembly.Load(providers[0]).GetType(providers[1]).GetField("Instance").GetValue(null),
                        node.Attributes["link"].Value);
                    continue;
                }

                Hashtable ht2 = node.ToHashtable();
                ht[node.Name] = ht2;

                LoadXml(node, ht2);
            }

        }

        public DbProvider GetDbFactory(string key)
        {
            return _dbs[key];
        }
    }
}
