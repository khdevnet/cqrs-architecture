using System;
using SW.Store.Checkout.Domain.Extensibility;
using SW.Store.Checkout.Infrastructure.SQL.Database;

namespace SW.Store.Checkout.Infrastructure.SQL.Repositories
{
    internal class OrderRepository : CrudRepository<Domain.Order, Guid>, IOrderRepository
    {

        public OrderRepository(SwStoreDbContext db) : base(db)
        {

        }
    }
}
