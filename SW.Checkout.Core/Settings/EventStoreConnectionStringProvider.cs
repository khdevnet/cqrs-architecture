using Microsoft.Extensions.Configuration;
using SW.Checkout.Core.Settings;

namespace SW.Checkout.Core.Settings
{
    public class EventStoreConnectionStringProvider : ConnectionStringProviderBase, IEventStoreConnectionStringProvider
    {
        public EventStoreConnectionStringProvider(IConfiguration configuration) : base(configuration, "EventStore")
        {
        }
    }
}
