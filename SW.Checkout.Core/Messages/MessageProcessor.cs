using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SW.Checkout.Core.Messages
{
    public class MessageProcessor : IMessageProcessor
    {
        private readonly IEnumerable<IMessageHandler> handlers;

        public MessageProcessor(IEnumerable<IMessageHandler> handlers)
        {
            this.handlers = handlers;
        }

        public void Process(IMessage message)
        {
            Type messageType = message.GetType();
            IMessageHandler handler = GetHandler(messageType);

            if (handler == null)
            {
                throw new NotImplementedException($"Message handler for message type {messageType.Name} not registered.");
            }

            MethodInfo handleMethod = handler.GetType().GetMethods().FirstOrDefault(m => m.GetParameters().Any(p => p.ParameterType == messageType));

            handleMethod.Invoke(handler, new[] { message });
        }

        private IMessageHandler GetHandler(Type messageType)
        {
            Func<IMessageHandler, IEnumerable<Type>> getInterfaceGenericArgs = (h) => h.GetType().GetInterfaces().SelectMany(inter => inter.GenericTypeArguments);

            return handlers.FirstOrDefault(h => getInterfaceGenericArgs(h).Any(arg => arg == messageType));
        }
    }
}
