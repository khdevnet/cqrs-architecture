using System;
using SW.Store.Core;

namespace SW.Store.Checkout.WebApi
{
    internal class FakeLogger : ILogger
    {
        public void Error(string message, Exception ex = null)
        {

        }
    }
}
