using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace Fas.Logging
{
    public class Logger : ILogger
    {
        static StackTrace st = new StackTrace();

        private static Logger Instance { get; } = new Logger();

        static Logger()
        {

        }

        private Logger()
        {

        }

        private static Type t;

        private Logger UseType<T>()
        {
            t = typeof(T);
            return this;
        }

        private static readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        public void Debug(string msg, params object[] args)
        {
#if (DEBUG)
            WriteToFile(string.Format(msg, args), "Debug");
#endif
        }

        public void Error(string msg, params object[] args)
        {
            WriteToFile(string.Format(msg, args), "Error");
        }

        public void Fatal(string msg, params object[] args)
        {
            WriteToFile(string.Format(msg, args), "Fatal");
        }

        public void Info(string msg, params object[] args)
        {
            WriteToFile(string.Format(msg, args), "Info");
        }

        public void Warn(string msg, params object[] args)
        {
            WriteToFile(string.Format(msg, args), "Warn");
        }

        private static void WriteToFile(string msg, string level)
        {
            var fas = Config.Instance["fas"];

            string logPath = fas["logPath"];
            string logMsg = fas["logMsg"];

            _lock.EnterWriteLock();
            try
            {
                using StreamWriter writer = new StreamWriter(logPath, true, new UTF8Encoding(false, true));
                writer.WriteLineAsync(string.Format(logMsg, level, t.FullName, msg));
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public static ILogger GetLogger<T>()
        {
            return Instance.UseType<T>();
        }
    }
}
