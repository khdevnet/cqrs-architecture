using System;
using SW.Checkout.Core.Events;

namespace SW.Checkout.Domain.Warehouses.Events
{
    public class WarehouseItemQuantitySubstracted : IEvent
    {
        public Guid WarehouseId { get; set; }

        public int Quantity { get; set; }

        public int ProductId { get; set; }
    }
}
