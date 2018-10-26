using System.Collections.Generic;
using SW.Store.Checkout.Extensibility.Queues.ReadStorageSync;
using SW.Store.Core.Messages;

namespace SW.Store.Checkout.Infrastructure.RabbitMQ.Queues.ReadStorage
{
    internal class ReadStorageSyncMessageProcessor : MessageProcessor, IReadStorageSyncMessageProcessor
    {
        public ReadStorageSyncMessageProcessor(IEnumerable<IReadStorageSyncMessageHandler> handlers) : base(handlers)
        {
        }
    }
}
