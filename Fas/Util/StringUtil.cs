using Fas.Util.Extend;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Fas.Util
{
    public class StringUtil
    {
        public static string JoinFormData(Hashtable data, bool k = true, string join = "=", bool v = true, string link = "&")
        {
            StringBuilder sb = new StringBuilder();
            foreach (var key in data.Keys)
            {
                if (k) sb.Append(key);
                sb.Append(join);
                if (v) sb.Append(v);
                sb.Append(link);
            }
            return sb.ToString().TrimEnd('&');
        }

        public static double ToDouble(string str, double def = 0)
        {
            if (string.IsNullOrEmpty(str)) return def;

            if (double.TryParse(str, out double result)) return result;

            return def;
        }

        public static HttpClient SetAccept(HttpClient client, string str = "text/html;application/xhtml+xml;application/xml,0.9;image/webp;*/*,0.8")
        {
            string[] accepts = str.Split(";");
            for (int i = 0; i < accepts.Length; i++)
            {
                string[] values = accepts[i].Split(",");

                var mt = new MediaTypeWithQualityHeaderValue(values[0]);

                if (values.Length == 2) mt.Quality = values[1].ToDouble();

                client.DefaultRequestHeaders.Accept.Add(mt);
            }
            return client;
        }
    }
}
