using Microsoft.Extensions.Configuration;
using SW.Store.Core.Settings;

namespace SW.Store.Checkout.WebApi
{
    public class ReadStorageConnectionStringProvider : ConnectionStringProviderBase, IReadStorageConnectionStringProvider
    {
        private readonly IConfiguration configuration;

        public ReadStorageConnectionStringProvider(IConfiguration configuration) : base(configuration, "ReadStorage")
        {
            this.configuration = configuration;
        }

    }
}
