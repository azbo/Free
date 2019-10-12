namespace Fas
{
    public interface ILogger
    {
        void Debug(string msg, params object[] args);
        void Info(string msg, params object[] args);

        void Warn(string msg, params object[] args);

        void Error(string msg, params object[] args);

        void Fatal(string msg, params object[] args);
    }
}
