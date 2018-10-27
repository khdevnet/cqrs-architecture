using SW.Store.Core;
using SW.Store.Core.Messages;
using SW.Store.Core.Queues.ReadStorageSync;

namespace SW.Store.Checkout.Infrastructure.RabbitMQ.Queues.ReadStorage
{
    internal class ReadStorageSyncQueueSubscriber : QueueSubscriber<IReadStorageSyncQueueSettingsProvider, IReadStorageSyncMessageProcessor, IMessageDeserializer>, IReadStorageSyncQueueSubscriber
    {
        public ReadStorageSyncQueueSubscriber(
            IReadStorageSyncMessageProcessor messageProcessor,
            IReadStorageSyncQueueSettingsProvider queueSettingsProvider,
            IMessageDeserializer messageDeserializer, ILogger logger) : base(messageProcessor, queueSettingsProvider, messageDeserializer, logger)
        {

        }
    }
}
