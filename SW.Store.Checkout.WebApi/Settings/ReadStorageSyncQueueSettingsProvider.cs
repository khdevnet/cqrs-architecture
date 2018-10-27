using Microsoft.Extensions.Configuration;
using SW.Store.Core.Queues.ReadStorageSync;

namespace SW.Store.Checkout.WebApi
{
    public class ReadStorageSyncQueueSettingsProvider : QueueSettingsProviderBase, IReadStorageSyncQueueSettingsProvider
    {
        public ReadStorageSyncQueueSettingsProvider(IConfiguration configuration) : base(configuration, "ReadStorageSyncQueue")
        {
        }

    }
}
