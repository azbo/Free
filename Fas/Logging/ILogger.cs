using System;

namespace Fas
{
    /// <summary>
    /// Manages logging.
    /// </summary>
    /// <remarks>
    /// This is a facade for the different logging subsystems.
    /// It offers a simplified interface that follows IOC patterns
    /// and a simplified priority/level/severity abstraction. 
    /// </remarks>
    public interface ILogger
    {
        #region Debug

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <param name="message">The message to log</param>
        void Debug(string msg, params object[] args);

        /// <summary>
        /// Determines if messages of priority "debug" will be logged.
        /// </summary>
        /// <value>True if "debug" messages will be logged.</value> 
        bool IsDebugEnabled { get; }

        #endregion

        #region Info

        /// <summary>
        /// Logs an info message.
        /// </summary>
        /// <param name="message">The message to log</param>
        void Info(string msg, params object[] args);

        /// <summary>
        /// Determines if messages of priority "info" will be logged.
        /// </summary>
        /// <value>True if "info" messages will be logged.</value> 
        bool IsInfoEnabled { get; }

        #endregion

        #region Warn

        /// <summary>
        /// Logs a warn message.
        /// </summary>
        /// <param name="message">The message to log</param>
        void Warn(string msg, params object[] args);

        /// <summary>
        /// Determines if messages of priority "warn" will be logged.
        /// </summary>
        /// <value>True if "warn" messages will be logged.</value> 
        bool IsWarnEnabled { get; }

        #endregion

        #region Error

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">The message to log</param>
        void Error(string msg, params object[] args);

        /// <summary>
        /// Determines if messages of priority "error" will be logged.
        /// </summary>
        /// <value>True if "error" messages will be logged.</value> 
        bool IsErrorEnabled { get; }

        #endregion

        #region Fatal

        /// <summary>
        /// Logs a fatal message.
        /// </summary>
        /// <param name="message">The message to log</param>
        void Fatal(string msg, params object[] args);

        /// <summary>
        /// Determines if messages of priority "fatal" will be logged.
        /// </summary>
        /// <value>True if "fatal" messages will be logged.</value> 
        bool IsFatalEnabled { get; }

        #endregion
    }
}
