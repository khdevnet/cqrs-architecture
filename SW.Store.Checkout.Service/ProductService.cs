using SW.Store.Checkout.Domain;
using SW.Store.Checkout.Domain.Extensibility;
using SW.Store.Checkout.Extensibility.Dto;
using SW.Store.Checkout.Service.Extensibility;
using System.Collections.Generic;
using System.Linq;

namespace SW.Store.Checkout.Service
{
    internal class ProductService : IProductService
    {
        private readonly ICrudRepository<Product, int> productRepository;

        public ProductService(ICrudRepository<Product, int> productRepository)
        {
            this.productRepository = productRepository;
        }

        public IEnumerable<ProductDto> Get()
        {
            return productRepository.Get()
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name
                }).ToList();
        }
    }
}
