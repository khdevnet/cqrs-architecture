using System;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SW.Store.Core;
using SW.Store.Core.Messages;
using SW.Store.Core.Settings;
using SW.Store.Core.Settings.Dto;

namespace SW.Store.Checkout.Infrastructure.RabbitMQ
{
    public class QueueSubscriber<TQueueSettingsProvider, TMessageProcessor, TMessageDeserializer> : IQueueSubscriber
        where TMessageDeserializer : IMessageDeserializer
        where TMessageProcessor : IMessageProcessor
        where TQueueSettingsProvider : IQueueSettingsProvider
    {
        private readonly IConnectionFactory connectionFactory;
        private readonly IConnection connection;
        private readonly IModel channel;
        private readonly EventingBasicConsumer consumer;
        private readonly TMessageProcessor messageProcessor;
        private readonly TQueueSettingsProvider queueSettingsProvider;
        private readonly TMessageDeserializer messageDeserializer;
        private readonly ILogger logger;


        public QueueSubscriber(TMessageProcessor messageProcessor, TQueueSettingsProvider queueSettingsProvider, TMessageDeserializer messageDeserializer, ILogger logger)
        {
            QueueSettings settings = queueSettingsProvider.Get();
            connectionFactory = new ConnectionFactory() { HostName = settings.Host };
            connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
            channel.QueueDeclare(queue: settings.QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queue: settings.QueueName, autoAck: false, consumer: consumer);
            this.messageProcessor = messageProcessor;
            this.queueSettingsProvider = queueSettingsProvider;
            this.messageDeserializer = messageDeserializer;
            this.logger = logger;
        }

        public void Dispose()
        {
            connection.Dispose();
            channel.Dispose();
        }

        public void Subscribe()
        {
            consumer.Received += (model, ea) =>
            {
                MessageContext<IMessage> message = messageDeserializer.Deserialize(ea.Body);
                try
                {
                    logger.Log($"### Get message: {message.Data.GetType().Name} ###");
                    logger.LogObject(message);

                    messageProcessor.Process(message.Data);
                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    logger.Log($"### Message processed: {message.Data.GetType().Name} ###");
                }
                catch (Exception ex)
                {
                    channel.BasicReject(ea.DeliveryTag, true);

                    logger.Error($"Reject message with DeliveryTag: {ea.DeliveryTag}", ex);
                }
            };
            Console.WriteLine(" [*] Waiting for messages.");
        }
    }
}
