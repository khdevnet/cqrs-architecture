using SW.Store.Checkout.Domain;
using SW.Store.Checkout.Extensibility.Dto;
using System;
using System.Collections.Generic;

namespace SW.Store.Checkout.Service
{
    public interface ICheckoutService
    {
        Guid CreateOrder(Guid orderId, string customerName, string customerAddress, IEnumerable<OrderLineDto> orderLines);
    }
}
