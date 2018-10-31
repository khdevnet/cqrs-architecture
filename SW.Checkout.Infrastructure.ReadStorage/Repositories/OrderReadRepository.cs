using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SW.Checkout.Infrastructure.ReadStorage.Database;
using SW.Checkout.Read.Extensibility;
using SW.Checkout.Read.ReadView;

namespace SW.Checkout.Infrastructure.ReadStorage.Repositories
{
    internal sealed class OrderReadRepository : IOrderReadRepository
    {
        private readonly SwStoreReadDbContext db;

        public OrderReadRepository(SwStoreReadDbContext db)
        {
            this.db = db;
        }

        public OrderReadView GetById(Guid id)
        {
            return GetQueryable().FirstOrDefault(p => p.Id == id);

        }

        public IEnumerable<OrderReadView> Get()
        {
            return GetQueryable();
        }

        public IEnumerable<OrderReadView> GetByCustomer(int customerId)
        {
            return GetQueryable().Where(o => o.CustomerId == customerId);
        }

        private IQueryable<OrderReadView> GetQueryable()
        {
            return db.OrderViews.Include(nameof(OrderReadView.Lines));
        }

        public int GetCountByCustomer(int customerId)
        {
            return GetQueryable().Count(x => x.CustomerId == customerId);
        }
    }
}
