using SW.Store.Checkout.Domain;
using System.Collections.Generic;

namespace SW.Store.Checkout.Service.Extensibility
{
    public interface IProductService
    {
        IEnumerable<Product> Get();
    }
}
