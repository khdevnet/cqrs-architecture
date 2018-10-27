using System;
using System.Collections.Generic;
using System.Linq;
using SW.Store.Checkout.Domain.Orders.Enum;
using SW.Store.Checkout.Domain.Orders.Events;
using SW.Store.Core.Aggregates;

namespace SW.Store.Checkout.Domain.Orders
{
    public class OrderAggregate : EventSourcedAggregate
    {
        public string Status { get; private set; } = OrderStatus.NotExist.ToString();

        public int CustomerId { get; private set; }

        public List<OrderLine> Lines { get; private set; } = new List<OrderLine>();

        public OrderAggregate()
        {
        }

        public OrderAggregate(Guid orderId, int customerId)
        {
            var @event = new OrderCreated
            {
                OrderId = orderId,
                CustomerId = customerId,
                Status = OrderStatus.Created.ToString()
            };

            Apply(@event);
            Append(@event);
        }

        public void AddLine(OrderLine line)
        {
            var @event = new OrderLineAdded(Id, line.ProductId, line.Quantity, line.Status);
            Apply(@event);
            Append(@event);
        }

        public void RemoveLine(int productNumber)
        {
            var @event = new OrderLineRemoved(Id, productNumber);
            Apply(@event);
            Append(@event);
        }

        public void Apply(OrderCreated @event)
        {
            Id = @event.OrderId;
            CustomerId = @event.CustomerId;
            Status = @event.Status;
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
                Lines.Add(new OrderLine { ProductId = @event.ProductNumber, Quantity = @event.Quantity });
            }
        }

        public void Apply(OrderLineRemoved @event)
        {
            Lines.RemoveAll(x => x.ProductId == @event.ProductNumber);
        }
    }
}
