using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SW.Store.Checkout.Domain.Accounts.Commands;
using SW.Store.Checkout.Extensibility.Client;
using SW.Store.Checkout.Read.Extensibility;
using SW.Store.Core.Commands;
using SW.Store.Core.Messages;

namespace SW.Store.Checkout.WebApi.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly IMessageSender messageSender;
        private readonly IMapper mapper;
        private readonly ICommandBus commandBus;
        private readonly IOrderReadRepository orderReadRepository;

        public CheckoutController(
            IMessageSender messageSender,
            IMapper mapper,
            ICommandBus commandBus,
            IOrderReadRepository orderReadRepository)
        {
            this.messageSender = messageSender;
            this.mapper = mapper;
            this.commandBus = commandBus;
            this.orderReadRepository = orderReadRepository;
        }

        // POST api/orders
        [HttpPost]
        public void Post([FromBody] CreateOrderRequestModel createOrder)
        {
            commandBus.Send(new CreateOrder()
            {
                CustomerId = 1,
                OrderId = createOrder.OrderId,
                Lines = createOrder.Lines
            });
        }

        [HttpPut]
        [Route("add-line")]
        public void AddLine([FromBody] AddOrderLine addOrderLine)
        {
            commandBus.Send(addOrderLine);
        }

        [HttpPut]
        [Route("remove-line")]
        public void RemoveLine([FromBody] RemoveOrderLine removeOrderLine)
        {
            commandBus.Send(removeOrderLine);
        }

        ///GET api/orders/status
        [HttpGet]
        [Route("status/{id}")]
        public IActionResult Status([FromRoute] Guid id)
        {
            Read.OrderReadDto order = orderReadRepository.GetById(id);
            if (order != null)
            {
                return Ok(new OrderResponseModel()
                {
                    OrderId = order.OrderId,
                    Lines = order.Lines.Select(l => new OrderLineResponseModel
                    {
                        ProductName = l.ProductName,
                        ProductNumber = l.ProductNumber,
                        Quantity = l.Quantity,
                        Status = l.Status
                    })
                });
            }
            return NotFound();
        }
    }
}
