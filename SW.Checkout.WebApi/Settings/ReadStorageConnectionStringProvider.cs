using Microsoft.Extensions.Configuration;
using SW.Checkout.Core.Settings;

namespace SW.Checkout.WebApi
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
