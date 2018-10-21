using Autofac;
using SW.Store.Core.Commands;
using SW.Store.Core.Events;

namespace SW.Store.Core
{
    public class CoreAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CommandBus>().As<ICommandBus>();
            builder.RegisterType<EventBus>().As<IEventBus>();
        }
    }
}
