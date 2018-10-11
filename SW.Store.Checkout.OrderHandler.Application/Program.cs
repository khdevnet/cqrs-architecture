using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Newtonsoft.Json;
using SW.Store.Checkout.Extensibility.Dto;
using SW.Store.Checkout.Extensibility.Messages;
using SW.Store.Checkout.Infrastructure.RabbitMQ;
using SW.Store.Checkout.Infrastructure.SQL;
using SW.Store.Checkout.Infrastructure.SQL.Database;
using SW.Store.Checkout.Service;
using SW.Store.Core;
using SW.Store.Core.Messages;

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

            IMessageSubscriber<CreateOrderMessage> messageSubscriber = messageSubscriberFactory.Create<CreateOrderMessage>(HostName, QueueName, RoutingKey);

            messageSubscriber.Subscribe(createOrderMessage =>
            {
                Console.WriteLine($"### Get message: {typeof(CreateOrderMessage).Name} ###");
                Console.WriteLine(JsonConvert.SerializeObject(createOrderMessage));
                IEnumerable<OrderLineDto> orderLines = createOrderMessage.Lines.Select(l => new OrderLineDto { ProductNumber = l.ProductNumber, Quantity = l.Quantity });
                using (ILifetimeScope scope = container.BeginLifetimeScope())
                {
                    ICheckoutService checkoutService = scope.Resolve<ICheckoutService>();
                    checkoutService.CreateOrder(createOrderMessage.OrderId, createOrderMessage.CustomerName, createOrderMessage.CustomerAddress, orderLines);
                }
                Console.WriteLine($"### Message processed: {typeof(CreateOrderMessage).Name} ###");

            });
            Console.WriteLine(" [*] Waiting for messages.");

            Console.ReadLine();
            messageSubscriber.Dispose();
        }

        private static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ConsoleLogger>().As<ILogger>();
            builder.RegisterType<SwStoreDbContext>().AsSelf();
            builder.RegisterType<ConnectionStringProvider>().As<IConnectionStringProvider>();
            builder.RegisterModule<RabbitMQAutofacModule>();
            builder.RegisterModule<ServiceAutofacModule>();
            builder.RegisterModule<SQLAutofacModule>();
            return builder.Build();
        }
    }
}
