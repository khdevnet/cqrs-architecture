using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SW.Store.Core;
using SW.Store.Core.Messages;

namespace SW.Store.Checkout.Infrastructure.RabbitMQ
{
    public class MessageSubscriber<TMessage> : IMessageSubscriber<TMessage> where TMessage : IMessage
    {
        private readonly IConnectionFactory connectionFactory;
        private readonly IConnection connection;
        private readonly IModel channel;
        private readonly EventingBasicConsumer consumer;
        private readonly ILogger logger;

        public MessageSubscriber(string hostName, string queueName, string routingKey, ILogger logger)
        {
            connectionFactory = new ConnectionFactory() { HostName = hostName };
            connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
            channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
            this.logger = logger;
        }

        public void Dispose()
        {
            connection.Dispose();
            channel.Dispose();
        }

        private static TMessage GetMessage(byte[] messageBody)
        {
            string message = Encoding.UTF8.GetString(messageBody);
            return JsonConvert.DeserializeObject<TMessage>(message);
        }


        public void Subscribe(Action<TMessage> callback)
        {
            consumer.Received += (model, ea) =>
            {
                TMessage message = GetMessage(ea.Body);
                try
                {
                    callback(message);
                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
                catch (Exception ex)
                {
                    channel.BasicReject(ea.DeliveryTag, true);

                    logger.Error($"Reject message with DeliveryTag: {ea.DeliveryTag}", ex);
                }
            };
        }
    }
}
