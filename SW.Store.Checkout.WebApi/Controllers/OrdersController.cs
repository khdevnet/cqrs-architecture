using Microsoft.AspNetCore.Mvc;
using SW.Store.Checkout.Client.Extensibility.Client;
using SW.Store.Checkout.Domain.Extensibility;
using SW.Store.Checkout.Extensibility.Client;
using SW.Store.Checkout.Extensibility.Dto;
using SW.Store.Checkout.Service;
using System;
using System.Linq;

namespace SW.Store.Checkout.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ICheckoutService checkoutService;
        private readonly ICustomerRepository customerRepository;
        private readonly IOrderRepository orderRepository;

        public OrdersController(ICheckoutService checkoutService, ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            this.checkoutService = checkoutService;
            this.customerRepository = customerRepository;
            this.orderRepository = orderRepository;
        }

        public ICustomerRepository CustomerRepository { get; }

        // POST api/Orders
        [HttpPost]
        public CreateOrderResponseModel Post([FromBody] OrderDto createOrder)
        {
            Guid createdOrderId = checkoutService.ProcessOrder(createOrder);
            Domain.Order order = orderRepository.GetById(createdOrderId, "Lines.Product");
            return new CreateOrderResponseModel()
            {
                OrderId = order.Id,
                Status = order.Status.ToString(),
                Lines = order.Lines.Select(l => new OrderLineResponseModel
                {
                    ProductName = l.Product.Name,
                    ProductNumber = l.ProductId,
                    Quantity = l.Quantity,
                    Status = l.LineStatus.ToString()
                })
            };
        }
    }
}
