using System;

namespace SW.Store.Core.Messages
{
    public interface IMessageSubscriberFactory
    {
        IMessageSubscriber<TMessage> Create<TMessage>(string hostName, string queueName, string routingKey) where TMessage : IMessage;
    }
}
