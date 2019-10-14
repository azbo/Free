using Fas.Util.Extend;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Fas.Util
{
    enum MediaType
    {
        form, json
    }
    public class HttpUtil
    {
        private static HttpClient http = new HttpClient();

        /// <summary>
        /// 使用Get方法获取字符串结果（没有加入Cookie）
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<string> GetAsync(string url, Encoding encoding = null)
        {
            if (encoding == null)
                return await http.GetStringAsync(url);
            return encoding.GetString(await http.GetByteArrayAsync(url));
        }

        /// <summary>
        /// Http Get 同步方法
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Get(string url, Encoding encoding = null)
        {
            if (encoding == null)
            {
                var strResult = http.GetStringAsync(url);
                strResult.Wait();
                return strResult.Result;
            }

            var byteResult = http.GetByteArrayAsync(url);
            byteResult.Wait();
            return encoding.GetString(byteResult.Result);
        }

        private void SetHeader()
        {

        }

        /// <summary>
        /// POST 异步
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postStream"></param>
        /// <param name="encoding"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<string> HttpPostAsync(string url, Hashtable data = null, Encoding encoding = null, int timeOut = 10000)
        {
            HttpClient client = new HttpClient(new HttpClientHandler()).SetAccept();
            HttpContent hc = new StreamContent(StreamUtil.WriteStream(new MemoryStream(), data.ToFormData(), encoding));
            hc.Headers.Add("UserAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36");
            hc.Headers.Add("Timeout", timeOut.ToString());
            hc.Headers.Add("KeepAlive", "true");

            var r = await client.PostAsync(url, hc);
            byte[] tmp = await r.Content.ReadAsByteArrayAsync();

            return encoding.GetString(tmp);
        }

        /// <summary>
        /// POST 同步
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postStream"></param>
        /// <param name="encoding"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static string Post(string url, Hashtable data = null, Encoding encoding = null, int timeOut = 10000)
        {
            HttpClient client = new HttpClient(new HttpClientHandler()).SetAccept();
            HttpContent hc = new StreamContent(StreamUtil.WriteStream(new MemoryStream(), data.ToFormData(), encoding));
            hc.Headers.Add("UserAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36");
            hc.Headers.Add("Timeout", timeOut.ToString());
            hc.Headers.Add("KeepAlive", "true");

            var t = client.PostAsync(url, hc);
            t.Wait();
            var t2 = t.Result.Content.ReadAsByteArrayAsync();
            return encoding.GetString(t2.Result);
        }
    }
}
