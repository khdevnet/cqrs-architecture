using System;
using System.Collections.Generic;
using SW.Store.Checkout.Extensibility.Dto;
using SW.Store.Core.Events;

namespace SW.Store.Checkout.Domain.Orders.Events
{
    public class OrderCreated : IEvent
    {
        public Guid OrderId { get; set; }

        public int CustomerId { get; set; }

        public string Status { get; set; }

        public ICollection<OrderLineDto> Lines { get; set; } = new List<OrderLineDto>();
    }
}
