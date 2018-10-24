using SW.Store.Core.Settings;
using SW.Store.Core.Settings.Dto;

namespace SW.Store.Checkout.OrderHandler.Application
{
    internal class QueueSettingsProvider : IQueueSettingsProvider
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
