using System.Collections.Generic;
using System.Linq;
using SW.Store.Checkout.Domain.Extensibility;
using SW.Store.Checkout.Infrastructure.SQL.Database;

namespace SW.Store.Checkout.Infrastructure.SQL.Repositories
{
    internal abstract class CrudRepository<TEntity, TId> : ICrudRepository<TEntity, TId> where TEntity : class
    {
        protected readonly SocksShopDbContext db;

        public CrudRepository(SocksShopDbContext db)
        {
            this.db = db;
        }

        public TEntity Add(TEntity entity)
        {
            db.Set<TEntity>().Add(entity);
            db.SaveChanges();
            return entity;
        }

        public IEnumerable<TEntity> Get()
        {
            return db.Set<TEntity>().ToList();
        }

        public TEntity GetById(TId id)
        {
            return db.Set<TEntity>().Find(id);
        }
    }
}
