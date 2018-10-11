using Microsoft.EntityFrameworkCore;
using SW.Store.Checkout.Domain;
using SW.Store.Checkout.Domain.Extensibility;
using SW.Store.Checkout.Infrastructure.SQL.Database;
using System;
using System.Linq;

namespace SW.Store.Checkout.Infrastructure.SQL.Repositories.Domain
{
    internal class OrderRepository : CrudRepository<Order, Guid>, IOrderRepository
    {

        public OrderRepository(SwStoreDbContext db) : base(db)
        {

        }

        public Order GetById(Guid id, string references)
        {
            return db.Set<Order>().Include(references).FirstOrDefault(x => x.Id == id);
        }
    }
}
