using System;
using System.Linq;
using SW.Checkout.Core.Queues.ReadStorageSync;
using SW.Checkout.Domain.Orders.Events;
using SW.Checkout.Domain.Warehouses.Events;
using SW.Checkout.Infrastructure.ReadStorage.Database;
using SW.Checkout.Read.ReadView;

namespace SW.Checkout.Infrastructure.ReadStorage.Synchronization
{
    internal class WarehouseSyncMessageHandler :
        IReadStorageSyncMessageHandler<WarehouseItemAdded>,
        IReadStorageSyncMessageHandler<WarehouseCreated>,
        IReadStorageSyncMessageHandler<WarehouseItemQuantitySubstracted>,
        IReadStorageSyncMessageHandler<OrderItemQuantityAdded>,
        IReadStorageSyncMessageHandler<OrderItemQuantitySubtracted>
    {
        private readonly SwStoreReadDbContext db;

        public WarehouseSyncMessageHandler(
            SwStoreReadDbContext db)
        {
            this.db = db;
        }

        public void Handle(WarehouseItemQuantitySubstracted message)
        {
            if (!IsItemExist(message.WarehouseId, message.ProductId))
            {
                return;
            }

            WarehouseItemReadView warehouseItem = GetItem(message.WarehouseId, message.ProductId);

            warehouseItem.Quantity -= message.Quantity;

            db.SaveChanges();
        }

        public void Handle(WarehouseItemAdded message)
        {
            if (IsItemExist(message.WarehouseId, message.ProductId))
            {
                return;
            }

            db.WarehouseItemReadViews.Add(new WarehouseItemReadView
            {
                ProductId = message.ProductId,
                Quantity = message.Quantity,
                WarehouseId = message.WarehouseId
            });
            db.SaveChanges();
        }

        public void Handle(WarehouseCreated message)
        {
            if (IsExist<WarehouseReadView>(message.WarehouseId))
            {
                return;
            }

            db.WarehouseReadViews.Add(new WarehouseReadView
            {
                Name = message.Name,
                Id = message.WarehouseId
            });
            db.SaveChanges();
        }

        private bool IsExist<TEntity>(object id) where TEntity : class
        {
            return db.Set<TEntity>().Find(id) != null;
        }

        private bool IsItemExist(Guid warehouseId, int productId)
        {
            return db.Set<WarehouseReadView>().Find(warehouseId) != null && GetItem(warehouseId, productId) != null;
        }

        private WarehouseItemReadView GetItem(Guid warehouseId, int productId)
        {
            return db.WarehouseItemReadViews
                    .FirstOrDefault(item => item.WarehouseId == warehouseId && item.ProductId == productId);
        }

        public void Handle(OrderItemQuantitySubtracted message)
        {
            WarehouseItemReadView warehouseItem = db.WarehouseItemReadViews.FirstOrDefault(l => l.WarehouseId == message.WarehouseId && l.ProductId == message.ProductNumber);

            if (warehouseItem != null)
            {
                warehouseItem.Quantity += message.Quantity;
                db.SaveChanges();
            }
        }

        public void Handle(OrderItemQuantityAdded message)
        {
            OrderLineReadView orderLine = db.OrderLineViews.FirstOrDefault(l => l.OrderId == message.OrderId && l.ProductId == message.ProductNumber);
            if (orderLine == null)
            {
                return;
            }

            WarehouseItemReadView warehouse = db.WarehouseItemReadViews.FirstOrDefault(l => l.WarehouseId == orderLine.WarehouseId && l.ProductId == message.ProductNumber);

            if (warehouse != null)
            {
                warehouse.Quantity -= message.Quantity;
                db.SaveChanges();
            }

        }
    }
}
