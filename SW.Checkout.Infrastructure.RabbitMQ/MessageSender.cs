using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using SW.Checkout.Core.Messages;

namespace SW.Checkout.Infrastructure.RabbitMQ
{
    internal class MessageSender : IMessageSender
    {
        public void Send<TMessage>(string hostName, string queueName, string routingKey, MessageContext<TMessage> message) where TMessage : IMessage
        {
            var factory = new ConnectionFactory() { HostName = hostName };
            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

                byte[] body = GetMessageBytes<TMessage>(message);

                channel.BasicPublish(exchange: "", routingKey: routingKey, basicProperties: null, body: body);
            }
        }

        private static byte[] GetMessageBytes<TMessage>(MessageContext<TMessage> message) where TMessage : IMessage
        {
            string messageStr = JsonConvert.SerializeObject(message);
            return Encoding.UTF8.GetBytes(messageStr);
        }
    }
}
