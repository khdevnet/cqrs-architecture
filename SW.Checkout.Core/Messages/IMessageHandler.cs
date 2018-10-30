namespace SW.Checkout.Core.Messages
{
    public interface IMessageHandler<in TMessage> : IMessageHandler
        where TMessage : IMessage
    {
        void Handle(TMessage message);
    }

    public interface IMessageHandler
    {
    }
}
