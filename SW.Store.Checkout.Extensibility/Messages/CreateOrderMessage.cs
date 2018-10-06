using SW.Store.Core.Messages;

namespace SW.Store.Checkout.Extensibility.Messages
{
    public class CreateOrderMessage : IMessage
    {
        public string Data { get; set; }
    }
}
