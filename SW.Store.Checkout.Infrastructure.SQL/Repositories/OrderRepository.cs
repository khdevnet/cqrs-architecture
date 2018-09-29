using System;
using CQRS.Socks.Order.Domain.Extensibility;
using CQRS.Socks.Order.Infrastructure.SQL.Database;

namespace CQRS.Socks.Order.Infrastructure.SQL.Repositories
{
    internal class OrderRepository : CrudRepository<Domain.Order, Guid>, IOrderRepository
    {

        public OrderRepository(SocksShopDbContext db) : base(db)
        {

        }
    }
}
