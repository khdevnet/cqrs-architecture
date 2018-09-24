using CQRS.Socks.Order.Client.Extensibility.Models;
using CQRS.Socks.Order.Domain.Extensibility;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Socks.Order.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository orderRepository;
        private readonly ICustomerRepository customerRepository;

        public OrdersController(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            this.orderRepository = orderRepository;
            this.customerRepository = customerRepository;
        }

        public ICustomerRepository CustomerRepository { get; }

        // POST api/values
        [HttpPost]
        public CreateOrderResponseModel Post([FromBody] CreateOrderModel createOrder)
        {
            var createdOrder = orderRepository.Add(new Domain.Order()
            {
                Id = createOrder.OrderId,
                Customer = customerRepository.Get(createOrder.CustomerName, createOrder.CustomerAddress)
            });
            return new CreateOrderResponseModel() { OrderId = createdOrder.Id };
        }
    }
}
