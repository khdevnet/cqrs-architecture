using System;
using SW.Store.Core;

namespace SW.Store.Checkout.OrderHandler.Application
{
    internal class ConsoleLogger : ILogger
    {
        public void Error(string message, Exception ex = null)
        {
            Console.WriteLine("##########################################");
            Console.WriteLine(message);
        }
    }
}
