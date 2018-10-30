using System;
using Marten.Events.Projections;
using SW.Checkout.Domain.Orders.Events;
using SW.Checkout.Domain.Orders.Views;

namespace SW.Checkout.Infrastructure.EventStore.ViewProjections.Orders
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
