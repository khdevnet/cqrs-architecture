using Microsoft.Extensions.Configuration;

namespace SW.Checkout.Core.Settings
{
    public class ConnectionStringProviderBase
    {
        private readonly IConfiguration configuration;
        private readonly string sectionName;

        public ConnectionStringProviderBase(IConfiguration configuration, string sectionName)
        {
            this.configuration = configuration;
            this.sectionName = sectionName;
        }

        public string Get()
        {
            string connection = configuration.GetSection(sectionName)["ConnectionString"];
            return connection;
        }
    }
}
