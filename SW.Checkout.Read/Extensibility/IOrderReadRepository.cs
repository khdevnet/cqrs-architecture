using System;
using System.Collections.Generic;
using SW.Store.Checkout.Read.ReadView;

namespace SW.Store.Checkout.Read.Extensibility
{
    public interface IOrderReadRepository
    {
        OrderReadView GetById(Guid id);

        IEnumerable<OrderReadView> Get();
    }
}
