using System;

namespace SW.Store.Core.Messages
{
    public interface IMessageSubscriber<TMessage> : IDisposable where TMessage : IMessage
    {
        void Subscribe(Action<TMessage> callback);
    }
}
