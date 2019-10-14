using System;

namespace Fas
{
    public interface ILogger
    {
        void Log(string level, string msg, params object[] args);

        void Debug(string msg, params object[] args) => Log("Debug", msg, args);

        void Debug(Exception e) => Debug(e.ToString());

        void Info(string msg, params object[] args) => Log("Info", msg, args);
        void Info(Exception e) => Info(e.ToString());

        void Warn(string msg, params object[] args) => Log("Warn", msg, args);

        void Warn(Exception e) => Warn(e.ToString());

        void Error(string msg, params object[] args) => Log("Error", msg, args);
        void Error(Exception e) => Error(e.ToString());
        void Critical(string msg, params object[] args) => Log("Critical", msg, args);
        void Critical(Exception e) => Critical(e.ToString());
    }
}
