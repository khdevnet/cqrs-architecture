using System;
using SW.Store.Core.Events;

namespace SW.Store.Checkout.Domain.Orders.Events
{
    public class OrderLineRemoved : IEvent
    {

        public OrderLineRemoved(Guid orderId, int productNumber)
        {
            ProductNumber = productNumber;
        }

        public Guid OrderId { get; }

        public int ProductNumber { get; }
    }
}
