using Autofac;
using SW.Store.Checkout.Domain.Orders.Handlers;
using SW.Store.Core.Messages;

namespace SW.Store.Checkout.Domain
{
    public class DomainAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OrderCommandHandler>().As<IMessageHandler>();
        }
    }
}
