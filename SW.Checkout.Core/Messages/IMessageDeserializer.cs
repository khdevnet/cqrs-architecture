namespace SW.Store.Core.Messages
{
    public interface IMessageDeserializer
    {
        MessageContext<IMessage> Deserialize(byte[] message);
    }
}
