namespace SW.Checkout.Core.Messages
{
    public interface IMessageDeserializer
    {
        MessageContext<IMessage> Deserialize(byte[] message);
    }
}
