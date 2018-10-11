using SW.Store.Checkout.Domain;
using SW.Store.Checkout.Domain.Extensibility;
using SW.Store.Checkout.Infrastructure.SQL.Database;
using System.Linq;

namespace SW.Store.Checkout.Infrastructure.SQL.Repositories
{
    internal class WarehouseRepository : CrudRepository<Warehouse, int>, IWarehouseRepository
    {
        public WarehouseRepository(SwStoreDbContext db) : base(db)
        {

        }

        public Warehouse Get(int productId, int quantity)
        {
            return db.Warehouses.FirstOrDefault(w => w.Items.Where(it => it.ProductId == productId).Sum(it => it.Quantity) >= quantity);
        }
    }
}
