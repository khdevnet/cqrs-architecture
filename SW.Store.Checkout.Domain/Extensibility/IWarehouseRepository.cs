using System.Collections.Generic;

namespace SW.Store.Checkout.Domain.Extensibility
{
    public interface IWarehouseRepository : ICrudRepository<Warehouse, int>
    {
       IEnumerable<Warehouse> Get(string reference);
    }
}
