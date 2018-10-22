using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SW.Store.Checkout.Domain.Accounts.Commands;
using SW.Store.Checkout.Extensibility.Client;
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

        public CheckoutController(
            IMessageSender messageSender,
            IMapper mapper,
            ICommandBus commandBus)
        {
            this.messageSender = messageSender;
            this.mapper = mapper;
            this.commandBus = commandBus;
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

        // GET api/orders/status
        //[HttpGet]
        //[Route("status/{id}")]
        //public IActionResult Status([FromRoute] Guid id)
        //{
        //    Read.OrderReadDto order = orderReadRepository.GetById(id);
        //    if (order != null)
        //    {
        //        return Ok(new OrderResponseModel()
        //        {
        //            OrderId = order.OrderId,
        //            Status = order.Status.ToString(),
        //            Lines = order.Lines.Select(l => new OrderLineResponseModel
        //            {
        //                ProductName = l.ProductName,
        //                ProductNumber = l.ProductNumber,
        //                Quantity = l.Quantity,
        //                Status = l.Status
        //            })
        //        });
        //    }
        //    return NotFound();
        //}
    }
}
