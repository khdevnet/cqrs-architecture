using SW.Checkout.Core;
using SW.Checkout.Core.Messages;
using SW.Checkout.Core.Queues.ProcessOrder;

namespace SW.Checkout.Infrastructure.RabbitMQ.Queues.ProcessOrder
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
