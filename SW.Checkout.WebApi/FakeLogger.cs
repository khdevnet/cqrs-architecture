using System;
using SW.Checkout.Core;

namespace SW.Checkout.WebApi
{
    internal class FakeLogger : ILogger
    {
        public void Error(string message, Exception ex = null)
        {

        }

        public void Log(string message)
        {
            throw new NotImplementedException();
        }

        public void LogObject(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
