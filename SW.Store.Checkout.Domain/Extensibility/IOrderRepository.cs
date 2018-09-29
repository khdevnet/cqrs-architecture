using System;

namespace SW.Store.Checkout.Domain.Extensibility
{
    public interface IOrderRepository : ICrudRepository<Order, Guid>
    {
    }
}
