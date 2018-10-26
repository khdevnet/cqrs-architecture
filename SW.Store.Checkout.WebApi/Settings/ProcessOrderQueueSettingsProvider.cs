using Microsoft.Extensions.Configuration;
using SW.Store.Checkout.Extensibility.Queues.ProcessOrder;

namespace SW.Store.Checkout.WebApi
{
    public class ProcessOrderQueueSettingsProvider : QueueSettingsProviderBase, IProcessOrderQueueSettingsProvider
    {
        public ProcessOrderQueueSettingsProvider(IConfiguration configuration) : base(configuration, "ProcessOrderQueue")
        {
        }

    }
}
