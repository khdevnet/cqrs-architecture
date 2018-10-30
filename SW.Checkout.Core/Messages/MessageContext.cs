namespace SW.Checkout.Core.Messages
{
    public class MessageContext<TMessage> where TMessage : IMessage
    {
        public MessageContext(string version, TMessage data) 
        {
            Version = version;
            Data = data;
        }

        public string Version { get; }

        public string MessageType => Data.GetType().FullName;

        public TMessage Data { get; set; }
    }
}
