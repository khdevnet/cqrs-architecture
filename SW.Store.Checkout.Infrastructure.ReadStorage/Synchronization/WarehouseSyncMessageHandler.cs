using System;
using System.Linq;
using SW.Store.Checkout.Domain.Warehouses.Events;
using SW.Store.Checkout.Extensibility.Queues.ReadStorageSync;
using SW.Store.Checkout.Infrastructure.ReadStorage.Database;
using SW.Store.Checkout.Read.ReadView;

namespace SW.Store.Checkout.Infrastructure.ReadStorage.Synchronization
{
    public class WarehouseSyncMessageHandler :
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

            WarehouseItemReadView warehouseItem = db.WarehouseItemReadViews
                    .FirstOrDefault(item => item.WarehouseId == message.WarehouseId && item.ProductId == message.ProductId);

            if (!IsExist<WarehouseReadView>(message.WarehouseId) || warehouseItem == null)
            {
                return;
            }

            warehouseItem.Quantity -= message.Quantity;

            db.SaveChanges();
        }

        private bool IsExist<TEntity>(Guid id) where TEntity : class
        {
            return db.Set<TEntity>().Find(id) != null;
        }

    }
}
