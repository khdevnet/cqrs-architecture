using System;

namespace SW.Store.Core
{
    public interface ILogger
    {
        void Error(string message, Exception ex = null);
    }
}
