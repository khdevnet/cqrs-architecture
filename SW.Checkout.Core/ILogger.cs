using System;

namespace SW.Checkout.Core
{
    public interface ILogger
    {
        void Error(string message, Exception ex = null);

        void Log(string message);

        void LogObject(object obj);
    }
}
