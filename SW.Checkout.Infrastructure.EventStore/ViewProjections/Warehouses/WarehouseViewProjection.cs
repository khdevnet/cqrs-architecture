using System;
using Marten.Events.Projections;
using SW.Checkout.Domain.Warehouses.Events;
using SW.Checkout.Domain.Warehouses.Views;

namespace SW.Checkout.Infrastructure.EventStore.ViewProjections.Warehouses
{
    public class WarehouseViewProjection : ViewProjection<WarehouseView, Guid>
    {
        public WarehouseViewProjection()
        {
            ProjectEvent<WarehouseCreated>((ev) => ev.WarehouseId, (view, @event) => view.Apply(@event));
            ProjectEvent<WarehouseItemQuantitySubstracted>((ev) => ev.WarehouseId, (view, @event) => view.Apply(@event));
            ProjectEvent<WarehouseItemAdded>((ev) => ev.WarehouseId, (view, @event) => view.Apply(@event));
        }
    }
}
