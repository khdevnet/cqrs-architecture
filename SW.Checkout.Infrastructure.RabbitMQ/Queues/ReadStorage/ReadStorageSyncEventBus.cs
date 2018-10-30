using SW.Checkout.Core.Events;
using SW.Checkout.Core.Messages;
using SW.Checkout.Core.Queues.ProcessOrder;
using SW.Checkout.Core.Queues.ReadStorageSync;
using SW.Checkout.Core.Settings.Dto;

namespace SW.Checkout.Infrastructure.RabbitMQ.Queues.ReadStorage
{
    internal class ReadStorageSyncEventBus : IReadStorageSyncEventBus
    {
        private readonly IMessageSender messageSender;
        private readonly IReadStorageSyncQueueSettingsProvider readStorageSyncQueueSettingsProvider;

        public ReadStorageSyncEventBus(
            IMessageSender messageSender,
            IReadStorageSyncQueueSettingsProvider readStorageSyncQueueSettingsProvider)
        {
            this.messageSender = messageSender;
            this.readStorageSyncQueueSettingsProvider = readStorageSyncQueueSettingsProvider;
        }

        public void Send<TEvent>(TEvent @event) where TEvent : IEvent
        {
            QueueSettings settings = readStorageSyncQueueSettingsProvider.Get();
            messageSender.Send(
                settings.Host,
                settings.QueueName,
                settings.Route,
                new MessageContext<TEvent>("v1", @event));
        }
    }
}
