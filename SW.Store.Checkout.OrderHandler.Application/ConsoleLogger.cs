using System;
using Newtonsoft.Json;
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

        public void Log(string message)
        {
            Console.WriteLine(message);
        }

        public void LogObject(object obj)
        {
            Console.WriteLine(JsonConvert.SerializeObject(obj));
        }
    }
}
