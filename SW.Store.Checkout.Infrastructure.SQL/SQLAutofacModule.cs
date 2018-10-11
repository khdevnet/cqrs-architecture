using Autofac;
using SW.Store.Checkout.Domain.Extensibility;
using SW.Store.Checkout.Infrastructure.SQL.Database;
using SW.Store.Checkout.Infrastructure.SQL.Repositories.Domain;

namespace SW.Store.Checkout.Infrastructure.SQL
{
    public class SQLAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SwStoreDbContext>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(SQLAutofacModule).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();

            builder
                .RegisterGeneric(typeof(CrudRepository<,>))
                .As(typeof(ICrudRepository<,>));
        }
    }
}
