using SW.Store.Checkout.Extensibility.Queues.ReadStorageSync;
using SW.Store.Core.Settings.Dto;

namespace SW.Store.Checkout.OrderHandler.Application
{
    internal class ReadStorageSyncQueueSettingsProvider : IReadStorageSyncQueueSettingsProvider
    {
        public QueueSettings Get()
        {
            return new QueueSettings
            {
                Host = "localhost",
                QueueName = "read-storage-sync",
                Route = "read-storage-sync"
            };
        }
    }
}
