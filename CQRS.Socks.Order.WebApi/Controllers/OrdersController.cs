using CQRS.Socks.Order.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Socks.Order.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        // POST api/values
        [HttpPost]
        public CreateOrderResponseModel Post([FromBody] CreateOrderModel createOrder)
        {
            return new CreateOrderResponseModel() { OrderId = createOrder.OrderId };
        }
    }
}
