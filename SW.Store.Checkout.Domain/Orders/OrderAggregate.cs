using SW.Store.Checkout.Domain.Orders.Events;
using SW.Store.Checkout.Extensibility;
using SW.Store.Checkout.Extensibility.Dto;
using SW.Store.Core.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SW.Store.Checkout.Domain.Orders
{
    public class OrderAggregate : EventSourcedAggregate
    {
        public string Status { get; private set; }

        public int CustomerId { get; private set; }

        public List<OrderLineDto> Lines { get; private set; } = new List<OrderLineDto>();

        public OrderAggregate()
        {
        }

        public OrderAggregate(Guid orderId, int customerId, IEnumerable<OrderLineDto> lines)
        {
            var @event = new OrderCreated
            {
                OrderId = orderId,
                CustomerId = customerId,
                Lines = lines.ToList(),
                Status = OrderStatus.Created.ToString()
            };

            Apply(@event);
            Append(@event);
        }

        public void AddLine(int productNumber, int quantity)
        {
            var @event = new OrderLineAdded(Id, productNumber, quantity);
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
