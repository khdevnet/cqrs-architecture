using SW.Store.Core.Messages;

namespace SW.Store.Checkout.Infrastructure.RabbitMQ
{
    internal class MessageSubscriberFactory : IMessageSubscriberFactory
    {
        public IMessageSubscriber<TMessage> Create<TMessage>(string hostName, string queueName, string routingKey) where TMessage : IMessage
        {
            return new MessageSubscriber<TMessage>(hostName, queueName, routingKey);
        }
    }
}
