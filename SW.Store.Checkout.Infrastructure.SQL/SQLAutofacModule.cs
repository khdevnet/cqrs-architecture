using Autofac;
using CQRS.Socks.Order.Infrastructure.SQL.Repositories;

namespace CQRS.Socks.Order.Infrastructure.SQL
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
