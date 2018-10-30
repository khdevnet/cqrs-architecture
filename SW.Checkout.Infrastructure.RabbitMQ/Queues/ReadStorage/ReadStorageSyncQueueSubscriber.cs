using SW.Checkout.Core;
using SW.Checkout.Core.Messages;
using SW.Checkout.Core.Queues.ReadStorageSync;

namespace SW.Checkout.Infrastructure.RabbitMQ.Queues.ReadStorage
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
