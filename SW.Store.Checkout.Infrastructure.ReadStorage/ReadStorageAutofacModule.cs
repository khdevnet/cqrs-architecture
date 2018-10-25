using Autofac;
using SW.Store.Checkout.Infrastructure.ReadStorage;
using SW.Store.Core;

namespace SW.Store.Checkout.Infrastructure.RabbitMQ
{
    public class ReadStorageAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DatabaseInitializer>().As<IInitializer>();
        }
    }
}
