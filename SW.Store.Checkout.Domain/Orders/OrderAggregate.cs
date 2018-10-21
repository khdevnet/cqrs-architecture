using System;
using System.Collections.Generic;
using System.Linq;
using SW.Store.Checkout.Domain.Orders.Events;
using SW.Store.Checkout.Extensibility;
using SW.Store.Checkout.Extensibility.Dto;
using SW.Store.Core.Aggregates;

namespace SW.Store.Checkout.Domain.Orders
{
    public class OrderAggregate : EventSourcedAggregate
    {
        public string Status { get; set; } = OrderStatus.Created.ToString();

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public ICollection<OrderLineDto> Lines { get; set; } = new List<OrderLineDto>();

        public OrderAggregate()
        {
        }

        public OrderAggregate(Guid orderId, int customerId, IEnumerable<OrderLineDto> lines)
        {
            var @event = new OrderCreated
            {
                OrderId = orderId,
                CustomerId = customerId,
                Lines = lines.ToList()
            };

            Apply(@event);
            Append(@event);
        }

        //public void RecordInflow(Guid fromId, decimal ammount)
        //{
        //    var @event = new NewInflowRecorded(fromId, Id, new Inflow(ammount, DateTime.Now));
        //    Apply(@event);
        //    Append(@event);
        //}

        //public void RecordOutflow(Guid toId, decimal ammount)
        //{
        //    var @event = new NewOutflowRecorded(Id, toId, new Outflow(ammount, DateTime.Now));
        //    Apply(@event);
        //    Append(@event);
        //}

        public void Apply(OrderCreated @event)
        {
            Id = @event.OrderId;
            CustomerId = @event.CustomerId;
            Lines = @event.Lines.ToList();
        }

        //public void Apply(NewInflowRecorded @event)
        //{
        //    Balance += @event.Inflow.Ammount;
        //}

        //public void Apply(NewOutflowRecorded @event)
        //{
        //    Balance -= @event.Outflow.Ammount;
        //}
    }
}
