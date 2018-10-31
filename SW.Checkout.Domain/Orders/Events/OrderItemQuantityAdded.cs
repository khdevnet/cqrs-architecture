using System;
using SW.Checkout.Core.Events;

namespace SW.Checkout.Domain.Orders.Events
{
    public class OrderItemQuantityAdded : IEvent
    {

        public OrderItemQuantityAdded(Guid orderId, Guid warehouseId, int productNumber, int quantity)
        {
            OrderId = orderId;
            WarehouseId = warehouseId;
            ProductNumber = productNumber;
            Quantity = quantity;
        }

        public Guid WarehouseId { get; }

        public Guid OrderId { get; }

        public int Quantity { get; }

        public int ProductNumber { get; }
    }
}
