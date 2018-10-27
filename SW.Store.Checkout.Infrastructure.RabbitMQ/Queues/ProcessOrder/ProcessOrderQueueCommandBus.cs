using System.Threading.Tasks;
using SW.Store.Core.Commands;
using SW.Store.Core.Messages;
using SW.Store.Core.Queues.ProcessOrder;
using SW.Store.Core.Settings.Dto;

namespace SW.Store.Checkout.Infrastructure.RabbitMQ.Queues.ProcessOrder
{
    internal class ProcessOrderQueueCommandBus : IProcessOrderQueueCommandBus
    {
        private readonly IMessageSender messageSender;
        private readonly IProcessOrderQueueSettingsProvider processOrderQueueSettingsProvider;

        public ProcessOrderQueueCommandBus(
            IMessageSender messageSender,
            IProcessOrderQueueSettingsProvider processOrderQueueSettingsProvider)
        {
            this.messageSender = messageSender;
            this.processOrderQueueSettingsProvider = processOrderQueueSettingsProvider;
        }

        public Task Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            QueueSettings settings = processOrderQueueSettingsProvider.Get();
            messageSender.Send(
                settings.Host,
                settings.QueueName,
                settings.Route,
                new MessageContext<TCommand>("v1", command));
            return Task.FromResult(0);
        }
    }
}
