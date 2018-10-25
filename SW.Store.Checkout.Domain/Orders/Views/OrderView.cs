using System;
using System.Collections.Generic;
using System.Linq;
using SW.Store.Checkout.Domain.Orders.Events;

namespace SW.Store.Checkout.Domain.Orders.Views
{
    public class OrderView
    {
        public string Status { get; set; }

        public Guid Id { get; set; }

        public int CustomerId { get; set; }

        public List<OrderLine> Lines { get; set; } = new List<OrderLine>();

        public void Apply(OrderCreated @event)
        {
            Id = @event.OrderId;
            CustomerId = @event.CustomerId;
            Status = @event.Status;
            Lines = @event.Lines.ToList();
        }

        public void Apply(OrderLineAdded @event)
        {
            OrderLine line = Lines.FirstOrDefault(x => x.ProductId == @event.ProductNumber);
            if (line != null)
            {
                line.Quantity += @event.Quantity;
            }
            else
            {
                Lines.Add(new OrderLine { ProductId = @event.ProductNumber, Quantity = @event.Quantity, Status = @event.Status });
            }
        }

        public void Apply(OrderLineRemoved @event)
        {
            Lines.RemoveAll(x => x.ProductId == @event.ProductNumber);
        }
    }
}
