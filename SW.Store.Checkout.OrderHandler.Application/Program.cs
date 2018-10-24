using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using SW.Store.Checkout.Domain.Orders.Handlers;
using SW.Store.Checkout.Infrastructure.EventStore;
using SW.Store.Checkout.Infrastructure.RabbitMQ;
using SW.Store.Checkout.Service;
using SW.Store.Core;
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

            IEnumerable<IQueueSubscriber> subscribers = container.Resolve<IEnumerable<IQueueSubscriber>>();
            subscribers.ToList().ForEach(s => s.Subscribe());

            Console.ReadLine();
            subscribers.ToList().ForEach(s => s.Dispose());
        }

        private static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType(typeof(OrderCommandHandler)).As<IMessageHandler>();
            builder.RegisterType(typeof(QueueSettingsProvider)).As<IQueueSettingsProvider>();
            builder.RegisterType<QueueSubscriber<IQueueSettingsProvider, IMessageProcessor, IMessageDeserializer>>().As<IQueueSubscriber>();
            builder.RegisterType<ConsoleLogger>().As<ILogger>();
            builder.RegisterType<ConnectionStringProvider>().As<IConnectionStringProvider>();
            builder.RegisterModule<RabbitMQAutofacModule>();
            builder.RegisterModule<ServiceAutofacModule>();
            builder.RegisterModule<EventStoreAutofacModule>();
            builder.RegisterModule<CoreAutofacModule>();
            return builder.Build();
        }
    }
}
