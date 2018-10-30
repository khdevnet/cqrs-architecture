using Microsoft.Extensions.Configuration;
using SW.Checkout.Core.Queues.ProcessOrder;
using SW.Checkout.Core.Settings;

namespace SW.Checkout.Core.Settings
{
    public class ProcessOrderQueueSettingsProvider : QueueSettingsProviderBase, IProcessOrderQueueSettingsProvider
    {
        public ProcessOrderQueueSettingsProvider(IConfiguration configuration) : base(configuration, "ProcessOrderQueue")
        {
        }

    }
}
