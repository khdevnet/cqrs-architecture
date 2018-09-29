using SW.Store.Checkout.Client.Extensibility.Models;
using SW.Store.Checkout.Domain.Extensibility;
using SW.Store.Checkout.Service;
using Microsoft.AspNetCore.Mvc;

namespace SW.Store.Checkout.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ICheckoutService checkoutService;
        private readonly ICustomerRepository customerRepository;

        public OrdersController(ICheckoutService checkoutService, ICustomerRepository customerRepository)
        {
            this.checkoutService = checkoutService;
            this.customerRepository = customerRepository;
        }

        public ICustomerRepository CustomerRepository { get; }

        // POST api/values
        [HttpPost]
        public CreateOrderResponseModel Post([FromBody] CreateOrderRequestModel createOrder)
        {
            var createdOrder = checkoutService.ProcessOrder(new Domain.Order()
            {
                Id = createOrder.OrderId,
                Customer = customerRepository.Get(createOrder.CustomerName, createOrder.CustomerAddress)
            });
            return new CreateOrderResponseModel() { OrderId = createdOrder.Id };
        }
    }
}
