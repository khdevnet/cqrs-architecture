namespace SW.Store.Core.Messages
{
    public class MessageContext<TMessage> where TMessage : IMessage
    {
        public MessageContext(string version, TMessage data) 
        {
            Version = version;
            Data = data;
        }

        public string Version { get; }

        public string MessageType => typeof(TMessage).FullName;

        public TMessage Data { get; set; }
    }
}
