using System;
using System.Collections.Generic;
using System.Linq;
using SW.Checkout.Domain.Warehouses.Events;
using SW.Checkout.Core.Views;

namespace SW.Checkout.Domain.Warehouses.Views
{
    public class WarehouseView : IView
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<WarehouseItem> Items { get; set; } = new List<WarehouseItem>();

        public void Apply(WarehouseCreated @event)
        {
            Id = @event.WarehouseId;
            Name = @event.Name;
        }

        public void Apply(WarehouseItemQuantitySubstracted @event)
        {
            Id = @event.WarehouseId;
            WarehouseItem warehouseItem = Items.FirstOrDefault(item => item.ProductId == @event.ProductId);
            warehouseItem.Quantity -= @event.Quantity;
        }

        public void Apply(WarehouseItemAdded @event)
        {
            Items.Add(new WarehouseItem
            {
                ProductId = @event.ProductId,
                Quantity = @event.Quantity
            });
        }
    }
}
