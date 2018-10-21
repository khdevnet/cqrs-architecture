using Microsoft.Extensions.Configuration;
using SW.Store.Core;

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
            string connection = configuration.GetConnectionString("EventStore");
            return connection;
        }
    }
}
