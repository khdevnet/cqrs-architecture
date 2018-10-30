namespace SW.Checkout.Core.Messages
{
    public interface IMessageSender
    {
        void Send<TMessage>(string hostName, string queueName, string routingKey, MessageContext<TMessage> message) where TMessage : IMessage;
    }
}
