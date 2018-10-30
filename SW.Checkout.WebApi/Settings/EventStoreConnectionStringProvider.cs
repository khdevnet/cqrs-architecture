using Microsoft.Extensions.Configuration;
using SW.Checkout.Core.Settings;

namespace SW.Checkout.WebApi
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
