using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using SW.Store.Core.Messages;

namespace SW.Store.Checkout.Infrastructure.RabbitMQ
{
    internal class MessageSender : IMessageSender
    {
        private const string HostName = "localhost";
        private const string QueueName = "processorder";
        private const string RoutingKey = "processorder";

        public void Send(IMessage message)
        {
            var factory = new ConnectionFactory() { HostName = HostName };
            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                byte[] body = GetMessageBytes(message);

                channel.BasicPublish(exchange: "", routingKey: RoutingKey, basicProperties: null, body: body);
            }
        }

        private static byte[] GetMessageBytes(IMessage message)
        {
            string messageStr = JsonConvert.SerializeObject(message);
            return Encoding.UTF8.GetBytes(messageStr);
        }
    }
}
