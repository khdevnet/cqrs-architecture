using Microsoft.EntityFrameworkCore;
using SW.Store.Checkout.Domain.Extensibility;
using SW.Store.Checkout.Infrastructure.SQL.Database;
using SW.Store.Checkout.Read.Extensibility;
using System.Collections.Generic;
using System.Linq;

namespace SW.Store.Checkout.Infrastructure.SQL.Repositories
{
    internal class ReadRepository<TEntity, TId> where TEntity : class
    {
        protected readonly SwStoreDbContext db;

        public ReadRepository(SwStoreDbContext db)
        {
            this.db = db;
        }

        protected IQueryable<TEntity> Get(string references = null)
        {
            return string.IsNullOrEmpty(references)
                ? db.Set<TEntity>()
                : db.Set<TEntity>().Include(references);
        }

        protected TEntity GetById(TId id)
        {
            return db.Set<TEntity>().Find(id);
        }
    }
}
