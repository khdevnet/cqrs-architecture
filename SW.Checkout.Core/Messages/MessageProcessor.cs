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
            var handlers = GetHandlers(messageType).ToList();

            if (handlers.Count == 0)
            {
                throw new NotImplementedException($"Message handler for message type {messageType.Name} not registered.");
            }

            handlers.ForEach((handler) => invokeMethod(handler, message, messageType));
        }

        private static void invokeMethod(IMessageHandler handler, IMessage message, Type messageType)
        {
            MethodInfo handleMethod = handler.GetType().GetMethods().FirstOrDefault(m => m.GetParameters().Any(p => p.ParameterType == messageType));

            handleMethod.Invoke(handler, new[] { message });
        }

        private IEnumerable<IMessageHandler> GetHandlers(Type messageType)
        {
            Func<IMessageHandler, IEnumerable<Type>> getInterfaceGenericArgs = (h) => h.GetType().GetInterfaces().SelectMany(inter => inter.GenericTypeArguments);

            return handlers.Where(h => getInterfaceGenericArgs(h).Any(arg => arg == messageType));
        }
    }
}
