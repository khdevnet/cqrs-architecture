using Autofac;
using SW.Checkout.Domain.Orders.Handlers;
using SW.Checkout.Core.Messages;

namespace SW.Checkout.Domain
{
    public class DomainAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OrderCommandHandler>().As<IMessageHandler>();
        }
    }
}
