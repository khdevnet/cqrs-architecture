using System;
using SW.Store.Core.Events;

namespace SW.Store.Checkout.Domain.Orders.Events
{
    public class OrderLineAdded : IEvent
    {

        public OrderLineAdded(Guid orderId, int productNumber, int quantity)
        {
            OrderId = orderId;
            ProductNumber = productNumber;
            Quantity = quantity;
        }

        public Guid OrderId { get; }

        public int ProductNumber { get; }

        public int Quantity { get; }
    }
}
