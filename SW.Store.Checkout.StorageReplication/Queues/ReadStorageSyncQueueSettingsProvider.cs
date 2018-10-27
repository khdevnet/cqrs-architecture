using SW.Store.Core.Queues.ReadStorageSync;
using SW.Store.Core.Settings.Dto;

namespace SW.Store.Checkout.StorageReplication
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
