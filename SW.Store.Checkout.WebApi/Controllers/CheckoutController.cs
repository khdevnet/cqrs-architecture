using Microsoft.AspNetCore.Mvc;
using SW.Store.Checkout.Client.Extensibility.Client;
using SW.Store.Checkout.Domain;
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
    public class CheckoutController : ControllerBase
    {
        private readonly ICheckoutService checkoutService;
        private readonly ICustomerRepository customerRepository;
        private readonly IOrderRepository orderRepository;

        public CheckoutController(ICheckoutService checkoutService, ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            this.checkoutService = checkoutService;
            this.customerRepository = customerRepository;
            this.orderRepository = orderRepository;
        }

        // POST api/Orders
        [HttpPost]
        public void Post([FromBody] OrderDto createOrder)
        {
            checkoutService.ProcessOrder(createOrder);
        }

        // POST api/Orders
        [HttpGet]
        [Route("status/{id}")]
        public IActionResult Status([FromRoute] Guid id)
        {
            Order order = orderRepository.GetById(id, "Lines.Product");
            if (order != null)
            {
                return Ok(new CreateOrderResponseModel()
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
                });
            }
            return NotFound();
        }
    }
}
