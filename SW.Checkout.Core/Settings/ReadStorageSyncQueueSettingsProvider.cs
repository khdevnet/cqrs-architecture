using Microsoft.Extensions.Configuration;
using SW.Checkout.Core.Queues.ReadStorageSync;
using SW.Checkout.Core.Settings;

namespace SW.Checkout.Core.Settings
{
    public class ReadStorageSyncQueueSettingsProvider : QueueSettingsProviderBase, IReadStorageSyncQueueSettingsProvider
    {
        public ReadStorageSyncQueueSettingsProvider(IConfiguration configuration) : base(configuration, "ReadStorageSyncQueue")
        {
        }

    }
}
