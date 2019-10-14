using System.Collections;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Fas.Util.Extend
{
    public static class StringExtend
    {
        public static double ToDouble(this string str, double def = 0) => StringUtil.ToDouble(str, def);

        public static string ToFormData(this Hashtable data) => data.JoinFormData();

        public static string JoinFormData(this Hashtable data, bool k = true, string join = "=", bool v = true, string link = "&") => StringUtil.JoinFormData(data, k, join, v, link);

        public static HttpClient SetAccept(this HttpClient client, string str = "") => StringUtil.SetAccept(client, str);
    }
}
