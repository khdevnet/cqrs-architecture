using Autofac;
using SW.Checkout.Core.Messages;

namespace SW.Checkout.Core
{
    public class CoreAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(MessageProcessor)).As<IMessageProcessor>();
            builder.RegisterType<MessageDeserializer>().As<IMessageDeserializer>();

        }
    }
}
