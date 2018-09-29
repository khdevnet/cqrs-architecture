using SW.Store.Checkout.Extensibility.Dto;
using System;

namespace SW.Store.Checkout.Service
{
    public interface ICheckoutService
    {
        Guid ProcessOrder(OrderDto order);
    }
}
