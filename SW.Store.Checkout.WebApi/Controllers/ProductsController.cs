using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SW.Store.Checkout.Extensibility.Client;
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
        public IEnumerable<ProductResponseModel> Get()
        {
            return productService.Get()
                .Select(p => new ProductResponseModel
                {
                    Id = p.Id,
                    Name = p.Name
                });
        }
    }
}
