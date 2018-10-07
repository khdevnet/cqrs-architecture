using System.Collections.Generic;
using SW.Store.Checkout.Domain;
using SW.Store.Checkout.Domain.Extensibility;
using SW.Store.Checkout.Service.Extensibility;

namespace SW.Store.Checkout.Service
{
    internal class ProductService : IProductService
    {
        private readonly ICrudRepository<Product, int> productRepository;

        public ProductService(ICrudRepository<Product, int> productRepository)
        {
            this.productRepository = productRepository;
        }

        public IEnumerable<Product> Get()
        {
            return productRepository.Get();
        }
    }
}
