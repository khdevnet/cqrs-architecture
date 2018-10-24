using SW.Store.Core;
using SW.Store.Core.Messages;

namespace SW.Store.Checkout.Infrastructure.RabbitMQ
{
    //internal class MessageSubscriberFactory : IMessageSubscriberFactory
    //{
    //    private readonly ILogger logger;

    //    public MessageSubscriberFactory(ILogger logger)
    //    {
    //        this.logger = logger;
    //    }

    //    public IQueueSubscriber<TMessage> Create<TMessage>(string hostName, string queueName, string routingKey) where TMessage : IMessage
    //    {
    //        return new MessageSubscriber<TMessage>(hostName, queueName, routingKey, this.logger);
    //    }
    //}
}
