using System.IO;
using System.Text;

namespace Fas.Util
{
    public class StreamUtil
    {
        public static Stream WriteStream(Stream stream, string str, Encoding encoding = null)
        {
            var data = string.IsNullOrEmpty(str) ? new byte[0] : encoding == null ? Encoding.UTF8.GetBytes(str) : encoding.GetBytes(str);
            stream.Write(data, 0, data.Length);
            stream.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return stream;
        }
    }
}
