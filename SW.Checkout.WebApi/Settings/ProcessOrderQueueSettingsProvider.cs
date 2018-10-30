using Microsoft.Extensions.Configuration;
using SW.Checkout.Core.Queues.ProcessOrder;

namespace SW.Checkout.WebApi
{
    public class ProcessOrderQueueSettingsProvider : QueueSettingsProviderBase, IProcessOrderQueueSettingsProvider
    {
        public ProcessOrderQueueSettingsProvider(IConfiguration configuration) : base(configuration, "ProcessOrderQueue")
        {
        }

    }
}
