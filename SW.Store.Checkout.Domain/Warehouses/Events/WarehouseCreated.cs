using System;
using SW.Store.Core.Events;

namespace SW.Store.Checkout.Domain.Warehouses.Events
{
    public class WarehouseCreated : IEvent
    {
        public Guid WarehouseId { get; set; }

        public string Name { get; set; }
    }
}
