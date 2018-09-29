using SW.Store.Checkout.Extensibility.Dto;
using System.Collections.Generic;

namespace SW.Store.Checkout.Service.Extensibility
{
    public interface IProductService
    {
        IEnumerable<ProductDto> Get();
    }
}
