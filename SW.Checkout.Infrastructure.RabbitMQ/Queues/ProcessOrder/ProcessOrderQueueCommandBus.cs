using System.Threading.Tasks;
using SW.Checkout.Core.Commands;
using SW.Checkout.Core.Messages;
using SW.Checkout.Core.Queues.ProcessOrder;
using SW.Checkout.Core.Settings.Dto;

namespace SW.Checkout.Infrastructure.RabbitMQ.Queues.ProcessOrder
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
