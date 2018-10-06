using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SW.Store.Checkout.Extensibility.Dto;
using SW.Store.Checkout.Extensibility.Messages;
using SW.Store.Checkout.Service.Extensibility;
using SW.Store.Core.Messages;

namespace SW.Store.Checkout.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IMessageSender messageBrocker;

        public ProductsController(IProductService productService, IMessageSender messageBrocker)
        {
            this.productService = productService;
            this.messageBrocker = messageBrocker;
        }

        [HttpGet]
        public IEnumerable<ProductDto> Get()
        {
            messageBrocker.Send(new CreateOrderMessage() { Data = "Test" });
            return productService.Get();
        }
    }
}
