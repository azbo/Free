using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Fas.Util.Extend
{
    public static class StringExtend
    {
        public static long ToTimeStamp(this DateTime time) => StringUtil.ToTimeStamp(time);
        public static DateTime ToDateTime(this long timeStamp) => StringUtil.ToDateTime(timeStamp);

        public static DateTime ToDateTime(this string str) => StringUtil.ToDateTime(str, DateTime.MinValue);

        public static DateTime ToDateTime(this string str, DateTime time) => StringUtil.ToDateTime(str, time);
        public static double ToDouble(this string str, double def = 0) => StringUtil.ToDouble(str, def);


        public static (string, string) ToKV(this string data, string join) => StringUtil.ToKV(data, join);

        public static List<(string, string)> ToKVList(this string data, string join, string link) => StringUtil.ToKVList(data, join, link);

        public static string Join(this Hashtable data, bool k = true, string join = "=", bool v = true, string link = "&") => StringUtil.Join(data, k, join, v, link);

        public static HttpClient SetAccept(this HttpClient client, string str = "") => StringUtil.SetAccept(client, str);

        public static HttpContent SetHeader(this HttpContent content, params string[] str) => StringUtil.SetHeader(content, str);
    }
}
