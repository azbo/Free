using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace Fas.Logging
{
    public class Logger : ILogger
    {
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

        public static ILogger GetLogger<T>() => Instance.UseType<T>();

        public void Log(string level, string msg, params object[] args)
        {
            msg = string.Format(msg, args);

            string logPath = "${dir}_zxzy_root_${@yyyyMMdd}.log";
            string logMsg = "${@yyyy-MM-dd HH:mm:ss} {0} {1} {2}";

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
    }
}
