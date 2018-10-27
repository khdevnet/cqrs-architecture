using SW.Store.Core.Queues.ProcessOrder;
using SW.Store.Core.Settings.Dto;

namespace SW.Store.Checkout.StorageReplication
{
    internal class ProcessOrderQueueSettingsProvider : IProcessOrderQueueSettingsProvider
    {
        public QueueSettings Get()
        {
            return new QueueSettings
            {
                Host = "localhost",
                QueueName = "processorder",
                Route = "processorder"
            };
        }
    }
}
