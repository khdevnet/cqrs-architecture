using SW.Checkout.Core.Settings;

namespace SW.Checkout.Message.Handler
{
    internal class EventStoreConnectionStringProvider : IEventStoreConnectionStringProvider
    {
        public string Get()
        {
            return "PORT = 5432; HOST = 127.0.0.1; TIMEOUT = 15; POOLING = True; MINPOOLSIZE = 1; MAXPOOLSIZE = 100; COMMANDTIMEOUT = 20; DATABASE = 'swstore'; PASSWORD = '123456'; USER ID = 'postgres'";
        }
    }
}
