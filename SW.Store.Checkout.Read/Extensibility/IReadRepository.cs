using System.Collections.Generic;

namespace SW.Store.Checkout.Read.Extensibility
{
    public interface IReadRepository<TEntity, TId> where TEntity : class
    {
        TEntity GetById(TId id);

        IEnumerable<TEntity> Get(string references = null);
    }
}
