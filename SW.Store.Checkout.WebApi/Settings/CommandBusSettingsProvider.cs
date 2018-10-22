using Microsoft.Extensions.Configuration;
using SW.Store.Core.Settings;
using SW.Store.Core.Settings.Dto;

namespace SW.Store.Checkout.WebApi
{
    public class CommandBusSettingsProvider : ICommandBusSettingsProvider
    {
        private readonly IConfiguration configuration;

        public CommandBusSettingsProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public CommandBusSettings Get()
        {
            IConfigurationSection section = configuration.GetSection("CommandBus");

            return new CommandBusSettings
            {
                Host = section.GetValue<string>(nameof(CommandBusSettings.Host)),
                QueueName = section.GetValue<string>(nameof(CommandBusSettings.QueueName)),
                Route = section.GetValue<string>(nameof(CommandBusSettings.Route)),
                Version = section.GetValue<string>(nameof(CommandBusSettings.Version)),
            };
        }
    }
}
