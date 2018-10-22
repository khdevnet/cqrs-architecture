using System.Threading.Tasks;
using SW.Store.Core.Messages;
using SW.Store.Core.Settings;
using SW.Store.Core.Settings.Dto;

namespace SW.Store.Core.Commands
{
    internal class CommandBus : ICommandBus
    {
        private readonly IMessageSender messageSender;
        private readonly ICommandBusSettingsProvider commandBusSettingsProvider;

        public CommandBus(
            IMessageSender messageSender,
            ICommandBusSettingsProvider commandBusSettingsProvider)
        {
            this.messageSender = messageSender;
            this.commandBusSettingsProvider = commandBusSettingsProvider;
        }

        public Task Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            CommandBusSettings settings = commandBusSettingsProvider.Get();
            messageSender.Send(
                settings.Host,
                settings.QueueName,
                settings.Route,
                new MessageContext<TCommand>(settings.Version, command));
            return Task.FromResult(0);
        }
    }
}
