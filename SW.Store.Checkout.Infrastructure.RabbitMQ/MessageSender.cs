using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using SW.Store.Core.Messages;

namespace SW.Store.Checkout.Infrastructure.RabbitMQ
{
    internal class MessageSender : IMessageSender
    {
        public void Send(string hostName, string queueName, string routingKey, IMessage message)
        {
            var factory = new ConnectionFactory() { HostName = hostName };
            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

                byte[] body = GetMessageBytes(message);

                channel.BasicPublish(exchange: "", routingKey: routingKey, basicProperties: null, body: body);
            }
        }

        private static byte[] GetMessageBytes(IMessage message)
        {
            string messageStr = JsonConvert.SerializeObject(message);
            return Encoding.UTF8.GetBytes(messageStr);
        }
    }
}
