using Microsoft.Extensions.Configuration;
using SW.Store.Core;
using SW.Store.Core.Settings;

namespace SW.Store.Checkout.WebApi
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        private readonly IConfiguration configuration;

        public ConnectionStringProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Get()
        {
            string connection = configuration.GetSection("EventStore")["ConnectionString"];
            return connection;
        }
    }
}
