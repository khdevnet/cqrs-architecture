using SW.Checkout.Core.Queues.ReadStorageSync;
using SW.Checkout.Core.Settings.Dto;

namespace SW.Checkout.StorageReplication
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
