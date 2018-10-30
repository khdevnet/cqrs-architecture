using Autofac;
using SW.Store.Core.Messages;

namespace SW.Store.Core
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
