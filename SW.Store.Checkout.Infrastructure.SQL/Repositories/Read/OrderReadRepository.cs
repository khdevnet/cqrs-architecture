using System;
using System.Collections.Generic;
using System.Linq;
using SW.Store.Checkout.Domain;
using SW.Store.Checkout.Infrastructure.SQL.Database;
using SW.Store.Checkout.Read;
using SW.Store.Checkout.Read.Extensibility;

namespace SW.Store.Checkout.Infrastructure.SQL.Repositories.Read
{
    internal class OrderReadRepository : ReadRepository<Order, Guid>, IOrderReadRepository
    {
        public OrderReadRepository(SwStoreDbContext db) : base(db)
        {

        }

        public IEnumerable<OrderReadDto> Get()
        {
            return Get("Order.Lines").Select(order => new OrderReadDto
            {
                OrderId = order.Id,
                Status = order.Status.ToString(),
                Lines = order.Lines.Select(l => new OrderLineReadDto
                {
                    ProductName = l.Product.Name,
                    ProductNumber = l.ProductId,
                    Quantity = l.Quantity,
                    Status = l.LineStatus.ToString()
                })
            });
        }

        public new OrderReadDto GetById(Guid id)
        {
            return Get().FirstOrDefault(p => p.OrderId == id);
        }
    }
}
