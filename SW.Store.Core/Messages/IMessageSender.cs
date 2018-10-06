using System;

namespace SW.Store.Core.Messages
{
    public interface IMessageSender
    {
        void Send(IMessage message);
    }
}
