using System;
using Autofac;
using SW.Store.Checkout.Extensibility.Messages;
using SW.Store.Checkout.Infrastructure.RabbitMQ;
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
            IMessageSubscriberFactory messageSubscriberFactory = CreateContainer().Resolve<IMessageSubscriberFactory>();

            IMessageSubscriber<CreateOrderMessage> messageSubscriber = messageSubscriberFactory.Create<CreateOrderMessage>(HostName, QueueName, RoutingKey);

            messageSubscriber.Subscribe(createOrderMessage =>
            {
                Console.WriteLine(createOrderMessage.Data);
            });
            Console.WriteLine(" [*] Waiting for messages.");

            //var factory = new ConnectionFactory() { HostName = "localhost" };
            //using (var connection = factory.CreateConnection())
            //using (var channel = connection.CreateModel())
            //{
            //    channel.QueueDeclare(queue: "processorder", durable: false, exclusive: false, autoDelete: false, arguments: null);

            //    Console.WriteLine(" [*] Waiting for messages.");

            //    var consumer = new EventingBasicConsumer(channel);
            //    consumer.Received += (model, ea) =>
            //    {
            //        var body = ea.Body;
            //        var message = Encoding.UTF8.GetString(body);
            //        Console.WriteLine(" [x] Received {0}", message);
            //    };
            //    channel.BasicConsume(queue: "processorder", autoAck: false, consumer: consumer);

            //    Console.WriteLine(" Press [enter] to exit.");
            //}

            Console.ReadLine();
            messageSubscriber.Dispose();
        }

        private static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<RabbitMQAutofacModule>();

            return builder.Build();
        }
    }
}
