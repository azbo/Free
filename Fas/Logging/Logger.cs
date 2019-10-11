using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Fas.Logging
{
    public class Logger : ILogger
    {
        private static readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        private static string type;

        public bool IsDebugEnabled
        {
            get
            {
#if (DEBUG)
                return true;
#else
                return false;
#endif
            }
        }

        public bool IsInfoEnabled { get; } = true;

        public bool IsWarnEnabled { get; } = true;

        public bool IsErrorEnabled { get; } = true;

        public bool IsFatalEnabled { get; } = true;

        public void Debug(string msg, params object[] args)
        {
#if (DEBUG)
            WriteToFile(string.Format(msg, args));
#endif
        }

        public void Error(string msg, params object[] args)
        {
            WriteToFile(string.Format(msg, args));
        }

        public void Fatal(string msg, params object[] args)
        {
            WriteToFile(string.Format(msg, args));
        }

        public void Info(string msg, params object[] args)
        {
            WriteToFile(string.Format(msg, args));
        }

        public void Warn(string msg, params object[] args)
        {
            WriteToFile(string.Format(msg, args));
        }

        private static void WriteToFile(string strContent)
        {
            Config config = Config.Instance;

            string filename = config["fas.log.file"];

            string dir = config["fas.dir"];
            if (string.IsNullOrEmpty(dir))
            {
                filename = filename.Replace("${dir}", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs"));
            }

            filename = Regex.Replace(filename, @"\$\{(\w+)\}", GetKey);

            _lock.EnterWriteLock();
            try
            {
                using StreamWriter writer = new StreamWriter(filename, true, new UTF8Encoding(false, true));
                writer.WriteAsync(strContent);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        private static string GetKey(Match match)
        {
            return Config.Instance[$"fas.{match.Groups[1].Value}"];
        }

        public static ILogger GetLogger<T>()
        {
            return new Logger();
        }
    }
}
