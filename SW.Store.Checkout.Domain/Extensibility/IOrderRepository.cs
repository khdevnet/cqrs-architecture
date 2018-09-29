using System;

namespace CQRS.Socks.Order.Domain.Extensibility
{
    public interface IOrderRepository : ICrudRepository<Order, Guid>
    {
    }
}
