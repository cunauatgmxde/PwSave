using System;
using gnCommon;

namespace gnLogger
{
    public interface ILogger : IDisposable
    {
        void Log(string message, LogLevel logLevel = LogLevel.DEFAULT);
        void Log(Exception ex, LogLevel logLevel = LogLevel.ERROR);
    }
}