using Fas.Util.Extend;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Fas.Util
{
    public class StringUtil
    {
        public static string Join(Hashtable data, bool k, string join, bool v, string link)
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


        public static long ToTimeStamp(DateTime time)
        {
            DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(time.AddHours(-8) - Jan1st1970).TotalMilliseconds;
        }

        public static DateTime ToDateTime(long timeStamp)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return start.AddMilliseconds(timeStamp).AddHours(8);
        }

        public static DateTime ToDateTime(string str, DateTime time)
        {
            if (time == null) return DateTime.MinValue;

            if (string.IsNullOrEmpty(str)) return time;

            if (DateTime.TryParse(str, out time)) return time;

            return time;
        }

        public static (string, string) ToKV(string data, string join)
        {
            var def = ("", "");
            if (string.IsNullOrEmpty(data) || string.IsNullOrEmpty(join)) return def;

            string[] strs = data.Split(join);
            return strs.Length == 2 ? (strs[0], strs[1]) : def;
        }

        public static List<(string, string)> ToKVList(string data, string join, string link)
        {
            if (string.IsNullOrEmpty(data) || string.IsNullOrEmpty(join) || string.IsNullOrEmpty(link)) return null;

            string[] splitDatas = data.Split(link);
            List<(string, string)> kvList = new List<(string, string)>();
            for (int i = 0; i < splitDatas.Length; i++)
            {
                kvList.Add((splitDatas[i].ToKV(join)));
            }
            return kvList;
        }

        public static double ToDouble(string str, double def = 0)
        {
            if (string.IsNullOrEmpty(str)) return def;

            if (double.TryParse(str, out double result)) return result;

            return def;
        }

        public static HttpClient SetAccept(HttpClient client, string str = "")
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

        public static HttpContent SetHeader(HttpContent content, params string[] header)
        {
            if (header == null || header.Length == 0) return content;

            for (int i = 0; i < header.Length; i = i + 2)
            {
                content.Headers.Add(header[i], header[i + 1]);
            }

            return content;
        }
    }
}
