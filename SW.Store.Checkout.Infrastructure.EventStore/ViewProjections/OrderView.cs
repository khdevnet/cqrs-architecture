using SW.Store.Checkout.Domain.Orders.Events;
using SW.Store.Checkout.Extensibility;
using SW.Store.Checkout.Extensibility.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SW.Store.Checkout.Domain.Orders
{
    public class OrderView
    {
        public string Status { get; set; } = OrderStatus.Created.ToString();

        public Guid Id { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public List<OrderLineDto> Lines { get; set; } = new List<OrderLineDto>();

        public void Apply(OrderCreated @event)
        {
            Id = @event.OrderId;
            CustomerId = @event.CustomerId;
            Status = @event.Status;
            Lines = @event.Lines.ToList();
        }

        public void Apply(OrderLineAdded @event)
        {
            OrderLineDto line = Lines.FirstOrDefault(x => x.ProductNumber == @event.ProductNumber);
            if (line != null)
            {
                line.Quantity += @event.Quantity;
            }
            else
            {
                Lines.Add(new OrderLineDto { ProductNumber = @event.ProductNumber, Quantity = @event.Quantity });
            }
        }

        public void Apply(OrderLineRemoved @event)
        {
            Lines.RemoveAll(x => x.ProductNumber == @event.ProductNumber);
        }
    }
}
