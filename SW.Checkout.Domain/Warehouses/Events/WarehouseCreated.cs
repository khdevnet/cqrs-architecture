using System;
using SW.Checkout.Core.Events;

namespace SW.Checkout.Domain.Warehouses.Events
{
    public class WarehouseCreated : IEvent
    {
        public Guid WarehouseId { get; set; }

        public string Name { get; set; }
    }
}
