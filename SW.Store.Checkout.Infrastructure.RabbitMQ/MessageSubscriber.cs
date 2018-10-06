using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SW.Store.Core.Messages;

namespace SW.Store.Checkout.Infrastructure.RabbitMQ
{
    public class MessageSubscriber<TMessage> : IMessageSubscriber<TMessage> where TMessage : IMessage
    {
        private readonly IConnectionFactory connectionFactory;
        private readonly IConnection connection;
        private readonly IModel channel;
        private readonly EventingBasicConsumer consumer;

        public MessageSubscriber(string hostName, string queueName, string routingKey)
        {
            connectionFactory = new ConnectionFactory() { HostName = hostName };
            connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
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
                callback(message);
            };
        }
    }
}
