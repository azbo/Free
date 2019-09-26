using System;
using System.IO;
using System.Net.Http;

namespace Fas.Config
{
    public class Config
    {
        static Config()
        {

        }

        private Config()
        {
            string rootpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");
            if (!File.Exists(Path.Combine(rootpath, "fas.conf"))) return;

            string url = Environment.GetEnvironmentVariable("fas_conf_url");
            if (string.IsNullOrEmpty(url)) return;

            var result = new HttpClient().GetAsync(url);


        }

        public static Config Instance { get; } = new Config();

        public void Load()
        {

        }
    }
}
