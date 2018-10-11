using System.Collections.Generic;

namespace SW.Store.Checkout.Read.Extensibility
{
    public interface IProductReadRepository
    {
        ProductReadDto GetById(int id);

        IEnumerable<ProductReadDto> Get(string references = null);
    }
}
