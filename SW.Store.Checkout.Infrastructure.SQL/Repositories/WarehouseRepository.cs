using Microsoft.EntityFrameworkCore;
using SW.Store.Checkout.Domain;
using SW.Store.Checkout.Domain.Extensibility;
using SW.Store.Checkout.Infrastructure.SQL.Database;
using System.Collections.Generic;
using System.Linq;

namespace SW.Store.Checkout.Infrastructure.SQL.Repositories
{
    internal class WarehouseRepository : CrudRepository<Warehouse, int>, IWarehouseRepository
    {
        public WarehouseRepository(SwStoreDbContext db) : base(db)
        {

        }

        public IEnumerable<Warehouse> Get(string reference)
        {
           return db.Set<Warehouse>().Include(reference).ToList();
        }
    }
}
