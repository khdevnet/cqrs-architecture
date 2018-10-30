using Microsoft.Extensions.Configuration;
using SW.Checkout.Core.Queues.ReadStorageSync;

namespace SW.Checkout.WebApi
{
    public class ReadStorageSyncQueueSettingsProvider : QueueSettingsProviderBase, IReadStorageSyncQueueSettingsProvider
    {
        public ReadStorageSyncQueueSettingsProvider(IConfiguration configuration) : base(configuration, "ReadStorageSyncQueue")
        {
        }

    }
}
