using System.Collections.Generic;
using System.Linq;

namespace SW.Store.Checkout.Read.Extensibility
{
    public interface IReadRepository<TEntity, TId> where TEntity : class
    {
        TEntity GetById(TId id);

        IQueryable<TEntity> Get(string references = null);
    }
}
