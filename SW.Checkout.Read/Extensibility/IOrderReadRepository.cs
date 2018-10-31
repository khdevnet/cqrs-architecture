using System;
using System.Collections.Generic;
using SW.Checkout.Read.ReadView;

namespace SW.Checkout.Read.Extensibility
{
    public interface IOrderReadRepository
    {
        OrderReadView GetById(Guid id);

        IEnumerable<OrderReadView> Get();

        IEnumerable<OrderReadView> GetByCustomer(int customerId);

        int GetCountByCustomer(int customerId);
    }
}
