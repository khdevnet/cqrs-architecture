using CQRS.Socks.Order.Domain.Extensibility;

namespace CQRS.Socks.Order.Service
{
    internal class CheckoutService : ICheckoutService
    {
        private readonly IOrderRepository orderRepository;

        public CheckoutService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public Domain.Order ProcessOrder(Domain.Order order)
        {
            return orderRepository.Add(order);
        }
    }
}
