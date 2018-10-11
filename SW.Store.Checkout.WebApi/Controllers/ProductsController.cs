using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SW.Store.Checkout.Extensibility.Client;
using SW.Store.Checkout.Read.Extensibility;

namespace SW.Store.Checkout.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRepository productReadRepository;

        public ProductsController(IProductReadRepository productReadRepository)
        {
            this.productReadRepository = productReadRepository;
        }

        [HttpGet]
        public IEnumerable<ProductResponseModel> Get()
        {
            return productReadRepository.Get()
                .Select(p => new ProductResponseModel
                {
                    Id = p.Id,
                    Name = p.Name
                });
        }
    }
}
