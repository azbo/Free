using Fas.Config;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string result = "[{\"$key1\":\"value1\"},{\"$key2\":\"value2\"},{\"$key3\":\"value3\"}]";

            string confPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "fas.config");
            if (!File.Exists(confPath)) return;

            List<Dictionary<string, string>> list = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(result);

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
    }
}
