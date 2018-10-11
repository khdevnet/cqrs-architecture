using Microsoft.EntityFrameworkCore;
using SW.Store.Checkout.Domain.Extensibility;
using SW.Store.Checkout.Infrastructure.SQL.Database;
using System.Collections.Generic;
using System.Linq;

namespace SW.Store.Checkout.Infrastructure.SQL.Repositories.Domain
{
    internal class CrudRepository<TEntity, TId> : ICrudRepository<TEntity, TId> where TEntity : class
    {
        protected readonly SwStoreDbContext db;

        public CrudRepository(SwStoreDbContext db)
        {
            this.db = db;
        }

        public TEntity Add(TEntity entity)
        {
            db.Set<TEntity>().Add(entity);
            db.SaveChanges();
            return entity;
        }

        public IEnumerable<TEntity> Get(string references = null)
        {
            return string.IsNullOrEmpty(references)
                ? db.Set<TEntity>().ToList()
                : db.Set<TEntity>().Include(references).ToList();
        }

        public TEntity GetById(TId id)
        {
            return db.Set<TEntity>().Find(id);
        }
    }
}
