using System.Collections.Generic;
using System.Linq;
using SW.Store.Checkout.Domain;
using SW.Store.Checkout.Infrastructure.SQL.Database;
using SW.Store.Checkout.Read;
using SW.Store.Checkout.Read.Extensibility;

namespace SW.Store.Checkout.Infrastructure.SQL.Repositories.Read
{
    internal class ProductReadRepository : ReadRepository<Product, int>, IProductReadRepository
    {
        public ProductReadRepository(SwStoreDbContext db) : base(db)
        {

        }

        public new IEnumerable<ProductReadDto> Get(string references = null)
        {
            return base.Get(references).Select(p => new ProductReadDto
            {
                Id = p.Id,
                Name = p.Name
            });
        }

        public new ProductReadDto GetById(int id)
        {
            return Get().FirstOrDefault(p => p.Id == id);
        }
    }
}
