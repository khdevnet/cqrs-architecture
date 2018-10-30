namespace SW.Checkout.Core.Messages
{
    public interface IMessageProcessor
    {
       void Process(IMessage message);
    }
}
