namespace SW.Store.Core.Messages
{
    public interface IMessageProcessor
    {
       void Process(IMessage message);
    }
}
