using SW.Store.Checkout.Domain;

namespace SW.Store.Checkout.Service
{
    public interface ICheckoutService
    {
        Domain.Order ProcessOrder(Domain.Order order); 
    }
}
