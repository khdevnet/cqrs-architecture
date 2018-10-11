using System;
using System.Collections.Generic;

namespace SW.Store.Checkout.Read.Extensibility
{
    public interface IOrderReadRepository
    {
        OrderReadDto GetById(Guid id);

        IEnumerable<OrderReadDto> Get();
    }
}
