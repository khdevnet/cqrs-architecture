using System;
using SW.Store.Core.Events;

namespace SW.Store.Checkout.Domain.Warehouses.Events
{
    public class WarehouseItemAdded : IEvent
    {
        public Guid WarehouseId { get; set; }

        public int Quantity { get; set; }

        public int ProductId { get; set; }
    }
}
