using System.Collections.Generic;
using SW.Checkout.Core.Messages;
using SW.Checkout.Core.Queues.ReadStorageSync;

namespace SW.Checkout.Infrastructure.RabbitMQ.Queues.ReadStorage
{
    internal class ReadStorageSyncMessageProcessor : MessageProcessor, IReadStorageSyncMessageProcessor
    {
        public ReadStorageSyncMessageProcessor(IEnumerable<IReadStorageSyncMessageHandler> handlers) : base(handlers)
        {
        }
    }
}
