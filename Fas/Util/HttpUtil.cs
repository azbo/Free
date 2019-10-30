using System.Collections;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Fas.Util
{
    enum MediaType
    {
        form, json
    }
    public class HttpUtil
    {
        private static string accepts = "text/html;application/xhtml+xml;application/xml,0.9;image/webp;*/*,0.8";
        private static string[] headers = new string[] { "UserAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36", "Timeout", "3000", "KeepAlive", "true" };

        private static HttpClient client = new HttpClient(new HttpClientHandler()).SetAccept(accepts);

        /// <summary>
        /// 使用Get方法获取字符串结果（没有加入Cookie）
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<string> GetAsync(string url, Encoding encoding = null)
        {
            if (encoding == null)
                return await client.GetStringAsync(url);
            return encoding.GetString(await client.GetByteArrayAsync(url));
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
                var strResult = client.GetStringAsync(url);
                strResult.Wait();
                return strResult.Result;
            }

            var byteResult = client.GetByteArrayAsync(url);
            byteResult.Wait();
            return encoding.GetString(byteResult.Result);
        }

        /// <summary>
        /// POST 异步
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postStream"></param>
        /// <param name="encoding"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<string> PostAsync(string url, Hashtable data = null, Encoding encoding = null)
        {
            HttpContent hc = new StreamContent(StreamUtil.WriteStream(new MemoryStream(), data.Join(), encoding ?? Encoding.UTF8)).SetHeader(headers);
            var ret = await client.PostAsync(url, hc);
            if (encoding == null)
            {
                return await ret.Content.ReadAsStringAsync();
            }
            return encoding.GetString(await ret.Content.ReadAsByteArrayAsync());
        }

        /// <summary>
        /// POST 同步
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postStream"></param>
        /// <param name="encoding"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static string Post(string url, Hashtable data = null, Encoding encoding = null)
        {
            HttpContent hc = new StreamContent(StreamUtil.WriteStream(new MemoryStream(), data.Join(), encoding ?? Encoding.UTF8)).SetHeader(headers);
            var ret = client.PostAsync(url, hc);
            ret.Wait();

            if (encoding == null)
                return ret.Result.Content.ReadAsStringAsync().Result;

            return encoding.GetString(ret.Result.Content.ReadAsByteArrayAsync().Result);
        }
    }
}
