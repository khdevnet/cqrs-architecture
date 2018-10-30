using System;
using SW.Checkout.Core.Events;

namespace SW.Checkout.Domain.Orders.Events
{
    public class OrderCreated : IEvent
    {
        public Guid OrderId { get; set; }

        public int CustomerId { get; set; }

        public string Status { get; set; }
    }
}
