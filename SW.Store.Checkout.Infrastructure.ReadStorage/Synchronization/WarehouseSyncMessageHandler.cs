using System;
using System.Linq;
using SW.Store.Checkout.Domain.Warehouses.Events;
using SW.Store.Checkout.Infrastructure.ReadStorage.Database;
using SW.Store.Checkout.Read.ReadView;
using SW.Store.Core.Queues.ReadStorageSync;

namespace SW.Store.Checkout.Infrastructure.ReadStorage.Synchronization
{
    internal class WarehouseSyncMessageHandler :
        IReadStorageSyncMessageHandler<WarehouseItemAdded>,
        IReadStorageSyncMessageHandler<WarehouseCreated>,
        IReadStorageSyncMessageHandler<WarehouseItemQuantitySubstracted>
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
    }
}
