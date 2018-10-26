using SW.Store.Checkout.Extensibility.Queues.ProcessOrder;
using SW.Store.Checkout.Extensibility.Queues.ReadStorageSync;
using SW.Store.Core.Events;
using SW.Store.Core.Messages;
using SW.Store.Core.Settings.Dto;

namespace SW.Store.Checkout.Infrastructure.RabbitMQ.Queues.ReadStorage
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
