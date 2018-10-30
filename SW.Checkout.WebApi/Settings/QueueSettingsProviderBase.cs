using Microsoft.Extensions.Configuration;
using SW.Checkout.Core.Settings.Dto;

namespace SW.Checkout.WebApi
{
    public class QueueSettingsProviderBase
    {
        private readonly IConfiguration configuration;
        private readonly string sectionName;

        public QueueSettingsProviderBase(IConfiguration configuration, string sectionName)
        {
            this.configuration = configuration;
            this.sectionName = sectionName;
        }

        public QueueSettings Get()
        {
            IConfigurationSection section = configuration.GetSection(sectionName);

            return new QueueSettings
            {
                Host = section.GetValue<string>(nameof(QueueSettings.Host)),
                QueueName = section.GetValue<string>(nameof(QueueSettings.QueueName)),
                Route = section.GetValue<string>(nameof(QueueSettings.Route)),
            };
        }
    }
}
