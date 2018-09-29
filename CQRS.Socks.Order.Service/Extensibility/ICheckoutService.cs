using CQRS.Socks.Order.Domain;

namespace CQRS.Socks.Order.Service
{
    public interface ICheckoutService
    {
        Domain.Order ProcessOrder(Domain.Order order); 
    }
}
