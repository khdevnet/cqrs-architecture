using System;

namespace SW.Store.Core
{
    public interface ILogger
    {
        void Error(string message, Exception ex = null);

        void Log(string message);

        void LogObject(object obj);
    }
}
