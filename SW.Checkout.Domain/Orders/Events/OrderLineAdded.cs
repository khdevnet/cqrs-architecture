using System;
using SW.Store.Checkout.Domain.Orders.Enum;
using SW.Store.Core.Events;

namespace SW.Store.Checkout.Domain.Orders.Events
{
    public class OrderLineAdded : IEvent
    {

        public OrderLineAdded(Guid orderId, int productNumber, int quantity, string status)
        {
            OrderId = orderId;
            ProductNumber = productNumber;
            Quantity = quantity;
            Status = status;
        }

        public Guid OrderId { get; }

        public int ProductNumber { get; }

        public string Status { get; } = OrderLineStatus.InStock.ToString();

        public int Quantity { get; }
    }
}
