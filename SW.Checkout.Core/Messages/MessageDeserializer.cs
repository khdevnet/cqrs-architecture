using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace SW.Checkout.Core.Messages
{
    public class MessageDeserializer : IMessageDeserializer
    {
        public MessageContext<IMessage> Deserialize(byte[] message)
        {
            string messageJson = Encoding.UTF8.GetString(message);

            var messageJObject = JObject.Parse(messageJson);
            JToken dataJToken = messageJObject[nameof(MessageContext<IMessage>.Data)];
            JToken messageTypeJToken = messageJObject[nameof(MessageContext<IMessage>.MessageType)];
            JToken verionJToken = messageJObject[nameof(MessageContext<IMessage>.Version)];

            Type messageType = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(ass => ass.GetTypes())
                .FirstOrDefault(t => t.FullName == messageTypeJToken.Value<string>());

            string verison = verionJToken.Value<string>();
            var data = (IMessage)dataJToken.ToObject(messageType);
            return new MessageContext<IMessage>(verison, data);
        }

    }
}
