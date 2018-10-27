using System;
using System.Collections.Generic;
using System.Linq;
using SW.Store.Checkout.Infrastructure.ReadStorage.Database;
using SW.Store.Checkout.Read.Extensibility;
using SW.Store.Checkout.Read.ReadView;

namespace SW.Store.Checkout.Infrastructure.ReadStorage.Repositories
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
            return Get().FirstOrDefault(p => p.Id == id);

        }

        public IEnumerable<OrderReadView> Get()
        {
            return db
             .Set<OrderReadView>()
             .Select(a => new OrderReadView
             {
                 Id = a.Id,
                 Lines = a.Lines.Select(l => new OrderLineReadView
                 {
                     ProductId = l.ProductId,
                     Quantity = l.Quantity,
                     Status = l.Status
                 }).ToList(),
             });
        }
    }
}
