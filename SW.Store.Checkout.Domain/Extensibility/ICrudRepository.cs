using System.Collections.Generic;

namespace SW.Store.Checkout.Domain.Extensibility
{
    public interface ICrudRepository<TEntity, TId> where TEntity : class
    {
        TEntity Add(TEntity entity);

        TEntity GetById(TId id);

        IEnumerable<TEntity> Get(string references = null);
    }
}
