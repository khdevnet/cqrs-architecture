using Microsoft.Extensions.Configuration;
using SW.Store.Core.Settings;

namespace SW.Store.Checkout.WebApi
{
    public class EventStoreConnectionStringProvider : ConnectionStringProviderBase, IEventStoreConnectionStringProvider
    {
        private readonly IConfiguration configuration;

        public EventStoreConnectionStringProvider(IConfiguration configuration) : base(configuration, "EventStore")
        {
            this.configuration = configuration;
        }
    }
}
