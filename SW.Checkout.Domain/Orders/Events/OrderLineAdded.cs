﻿using System;
using SW.Checkout.Domain.Orders.Enum;
using SW.Checkout.Core.Events;

namespace SW.Checkout.Domain.Orders.Events
{
    public class OrderLineAdded : IEvent
    {

        public OrderLineAdded(Guid orderId, Guid warehouseId, int productNumber, int quantity, string status)
        {
            OrderId = orderId;
            WarehouseId = warehouseId;
            ProductNumber = productNumber;
            Quantity = quantity;
            Status = status;
        }

        public Guid OrderId { get; }

        public Guid WarehouseId { get; }

        public int ProductNumber { get; }

        public string Status { get; } = OrderLineStatus.InStock.ToString();

        public int Quantity { get; }
    }
}
