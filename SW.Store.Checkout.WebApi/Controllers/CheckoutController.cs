using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SW.Store.Checkout.Client.Extensibility.Client;
using SW.Store.Checkout.Domain;
using SW.Store.Checkout.Domain.Extensibility;
using SW.Store.Checkout.Extensibility.Client;
using SW.Store.Checkout.Extensibility.Dto;
using SW.Store.Checkout.Extensibility.Messages;
using SW.Store.Core.Messages;

namespace SW.Store.Checkout.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly IMessageSender messageSender;
        private readonly ICustomerRepository customerRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;

        public CheckoutController(
            IMessageSender messageSender,
            ICustomerRepository customerRepository,
            IOrderRepository orderRepository,
            IMapper mapper)
        {
            this.messageSender = messageSender;
            this.customerRepository = customerRepository;
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }

        // POST api/Orders
        [HttpPost]
        public void Post([FromBody] OrderDto createOrder)
        {
            messageSender.Send("localhost", "processorder", "processorder", mapper.Map<CreateOrderMessage>(createOrder));
        }

        // POST api/Orders
        [HttpGet]
        [Route("status/{id}")]
        public IActionResult Status([FromRoute] Guid id)
        {
            Order order = orderRepository.GetById(id, "Lines.Product");
            if (order != null)
            {
                return Ok(new OrderResponseModel()
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
