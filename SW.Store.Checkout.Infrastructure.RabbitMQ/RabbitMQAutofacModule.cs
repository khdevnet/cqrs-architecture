using Autofac;
using SW.Store.Core.Commands;
using SW.Store.Core.Messages;

namespace SW.Store.Checkout.Infrastructure.RabbitMQ
{
    public class RabbitMQAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MessageSender>().As<IMessageSender>();
            builder.RegisterType<CommandBus>().As<ICommandBus>();

        }
    }
}
