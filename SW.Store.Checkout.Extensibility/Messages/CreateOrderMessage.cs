using System;
using System.Collections.Generic;
using SW.Store.Core.Messages;

namespace SW.Store.Checkout.Extensibility.Messages
{
    public class CreateOrderMessage : IMessage
    {
        public Guid OrderId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerAddress { get; set; }

        public IEnumerable<OrderLineMessage> Lines { get; set; }
    }
}
