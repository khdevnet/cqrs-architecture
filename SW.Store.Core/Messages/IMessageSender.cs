namespace SW.Store.Core.Messages
{
    public interface IMessageSender
    {
        void Send(string hostName, string queueName, string routingKey, IMessage message);
    }
}
