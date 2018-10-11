using SW.Store.Checkout.Domain;
using SW.Store.Checkout.Domain.Extensibility;
using SW.Store.Checkout.Infrastructure.SQL.Database;
using System.Linq;

namespace SW.Store.Checkout.Infrastructure.SQL.Repositories
{
    internal class WarehouseItemRepository : CrudRepository<WarehouseItem, int>, IWarehouseItemRepository
    {
        public WarehouseItemRepository(SwStoreDbContext db) : base(db)
        {

        }

        public WarehouseItem Get(int productId, int warehouseId)
        {
            return db.WarehouseItems.FirstOrDefault(wi => wi.ProductId == productId && wi.WarehouseId == warehouseId);
        }

        public WarehouseItem UpdateQuantity(int productId, int warehouseId, int quantity)
        {
            WarehouseItem itemEntity = db.WarehouseItems.FirstOrDefault(wi => wi.ProductId == productId && wi.WarehouseId == warehouseId);
            itemEntity.Quantity = quantity;
            db.SaveChanges();
            return itemEntity;
        }
    }
}
