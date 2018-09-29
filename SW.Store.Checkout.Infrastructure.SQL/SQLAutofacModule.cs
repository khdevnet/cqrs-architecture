using Autofac;
using SW.Store.Checkout.Infrastructure.SQL.Repositories;

namespace SW.Store.Checkout.Infrastructure.SQL
{
    public class SQLAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(CrudRepository<,>).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();
        }
    }
}
