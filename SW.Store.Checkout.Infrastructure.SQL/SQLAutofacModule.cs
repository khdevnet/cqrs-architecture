using Autofac;
using SW.Store.Checkout.Domain.Extensibility;
using SW.Store.Checkout.Infrastructure.SQL.Repositories;

namespace SW.Store.Checkout.Infrastructure.SQL
{
    public class SQLAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(SQLAutofacModule).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();

            builder
                .RegisterGeneric(typeof(CrudRepository<,>))
                .As(typeof(ICrudRepository<,>));
        }
    }
}
