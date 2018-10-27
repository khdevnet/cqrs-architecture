using SW.Store.Core;
using SW.Store.Core.Messages;
using SW.Store.Core.Queues.ProcessOrder;

namespace SW.Store.Checkout.Infrastructure.RabbitMQ.Queues.ProcessOrder
{
    internal class ProcessOrderQueueSubscriber : QueueSubscriber<IProcessOrderQueueSettingsProvider, IMessageProcessor, IMessageDeserializer>, IProcessOrderQueueSubscriber
    {
        public ProcessOrderQueueSubscriber(
            IMessageProcessor messageProcessor,
            IProcessOrderQueueSettingsProvider queueSettingsProvider,
            IMessageDeserializer messageDeserializer, ILogger logger) : base(messageProcessor, queueSettingsProvider, messageDeserializer, logger)
        {

        }
    }
}
