using System;
using System.Collections.Generic;
using System.Linq;
using SW.Store.Checkout.Domain.Warehouses.Events;
using SW.Store.Core.Aggregates;

namespace SW.Store.Checkout.Domain.Warehouses
{
    public class WarehouseAggregate : EventSourcedAggregate
    {
        public string Name { get; private set; }

        public List<WarehouseItem> Items { get; private set; } = new List<WarehouseItem>();

        public WarehouseAggregate()
        {
        }

        public WarehouseAggregate(Guid id, string name, IEnumerable<WarehouseItem> items)
        {
            var @event = new WarehouseCreated
            {
                WarehouseId = id,
                Name = name
            };

            Apply(@event);
            Append(@event);
            items.ToList().ForEach(it => AddItem(it.ProductId, it.Quantity));
        }

        public void SubstractItemQuantity(int productNumber, int quantity)
        {
            var @event = new WarehouseItemQuantitySubstracted
            {
                WarehouseId = Id,
                ProductId = productNumber,
                Quantity = quantity
            };
            Apply(@event);
            Append(@event);
        }

        public void AddItem(int productNumber, int quantity)
        {
            var @event = new WarehouseItemAdded
            {
                WarehouseId = Id,
                ProductId = productNumber,
                Quantity = quantity
            };

            Apply(@event);
            Append(@event);
        }


        public void Apply(WarehouseCreated @event)
        {
            Id = @event.WarehouseId;
            Name = @event.Name;
        }

        public void Apply(WarehouseItemAdded @event)
        {
            Items.Add(new WarehouseItem
            {
                ProductId = @event.ProductId,
                Quantity = @event.Quantity
            });
        }


        public void Apply(WarehouseItemQuantitySubstracted @event)
        {
            Id = @event.WarehouseId;
            WarehouseItem warehouseItem = Items.FirstOrDefault(item => item.ProductId == @event.ProductId);
            warehouseItem.Quantity -= @event.Quantity;
        }
    }
}
