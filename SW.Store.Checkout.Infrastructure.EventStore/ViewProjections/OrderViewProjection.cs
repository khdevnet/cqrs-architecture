using System;
using Marten.Events.Projections;
using SW.Store.Checkout.Domain.Orders;
using SW.Store.Checkout.Domain.Orders.Events;

namespace SW.Store.Checkout.Infrastructure.EventStore.ViewProjections
{
    public class OrderViewProjection : ViewProjection<OrderView, Guid>
    {
        public OrderViewProjection()
        {
            ProjectEvent<OrderCreated>((ev) => ev.OrderId, (view, @event) => view.Apply(@event));
            ProjectEvent<OrderLineAdded>((ev) => ev.OrderId, (view, @event) => view.Apply(@event));
            ProjectEvent<OrderLineRemoved>((ev) => ev.OrderId, (view, @event) => view.Apply(@event));
        }
    }
}
