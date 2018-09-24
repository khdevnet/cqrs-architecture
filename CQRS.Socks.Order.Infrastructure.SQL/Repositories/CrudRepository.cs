using System;
using CQRS.Socks.Order.Domain.Extensibility;
using CQRS.Socks.Order.Infrastructure.SQL.Database;

namespace CQRS.Socks.Order.Infrastructure.SQL.Repositories
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

        public TEntity GetById(TId id)
        {
            return db.Set<TEntity>().Find(id);
        }
    }
}
