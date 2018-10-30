using SW.Checkout.Core.Queues.ProcessOrder;
using SW.Checkout.Core.Settings.Dto;

namespace SW.Checkout.StorageReplication
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
