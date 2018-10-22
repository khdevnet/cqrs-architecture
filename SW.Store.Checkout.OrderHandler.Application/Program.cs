using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Newtonsoft.Json;
using SW.Store.Checkout.Domain.Accounts.Commands;
using SW.Store.Checkout.Domain.Orders.Handlers;
using SW.Store.Checkout.Infrastructure.EventStore;
using SW.Store.Checkout.Infrastructure.RabbitMQ;
using SW.Store.Checkout.Service;
using SW.Store.Core;
using SW.Store.Core.Commands;
using SW.Store.Core.Messages;
using SW.Store.Core.Settings;

namespace SW.Store.Checkout.OrderHandler.Application
{
    class Program
    {
        private const string HostName = "localhost";
        private const string QueueName = "processorder";
        private const string RoutingKey = "processorder";

        static void Main(string[] args)
        {
            IContainer container = CreateContainer();
            IMessageSubscriberFactory messageSubscriberFactory = container.Resolve<IMessageSubscriberFactory>();

            IMessageSubscriber<CreateOrder> messageSubscriber = messageSubscriberFactory.Create<CreateOrder>(HostName, QueueName, RoutingKey);

            messageSubscriber.Subscribe(createOrder =>
            {
                // TODO: Version support
                Console.WriteLine($"### Get message: {typeof(CreateOrder).Name} ###");
                Console.WriteLine(JsonConvert.SerializeObject(createOrder));
                // IEnumerable<OrderLineDto> orderLines = createOrderMessage.Data.Lines.Select(l => new OrderLineDto { ProductNumber = l.ProductNumber, Quantity = l.Quantity });
                var handlers = container.Resolve<IEnumerable<IMessageHandler>>();

                var handler = handlers.FirstOrDefault(h => h.GetType().GetInterfaces().Any(intf => intf.GetGenericArguments().Any(arg => arg == typeof(CreateOrder))));

                var method = handler.GetType().GetMethod(nameof(IMessageHandler<IMessage>.Handle));

                method.Invoke(handler, new[] { createOrder.Data });

                //checkoutService.CreateOrder(createOrderMessage.Data.OrderId, createOrderMessage.Data.CustomerName, createOrderMessage.Data.CustomerAddress, orderLines);
                Console.WriteLine($"### Message processed: {typeof(CreateOrder).Name} ###");

            });
            Console.WriteLine(" [*] Waiting for messages.");

            Console.ReadLine();
            messageSubscriber.Dispose();
        }

        private static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType(typeof(OrderCommandHandler)).As(typeof(IMessageHandler));
            builder.RegisterType<ConsoleLogger>().As<ILogger>();
            builder.RegisterType<ConnectionStringProvider>().As<IConnectionStringProvider>();
            builder.RegisterModule<RabbitMQAutofacModule>();
            builder.RegisterModule<ServiceAutofacModule>();
            builder.RegisterModule<EventStoreAutofacModule>();
            return builder.Build();
        }
    }
}
