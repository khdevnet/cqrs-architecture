using System.Collections.Generic;
using SW.Store.Core.Messages;
using SW.Store.Core.Queues.ReadStorageSync;

namespace SW.Store.Checkout.Infrastructure.RabbitMQ.Queues.ReadStorage
{
    internal class ReadStorageSyncMessageProcessor : MessageProcessor, IReadStorageSyncMessageProcessor
    {
        public ReadStorageSyncMessageProcessor(IEnumerable<IReadStorageSyncMessageHandler> handlers) : base(handlers)
        {
        }
    }
}
