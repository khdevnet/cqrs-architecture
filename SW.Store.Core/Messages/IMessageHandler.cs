using SW.Store.Core.Messages;

namespace SW.Store.Core.Commands
{
    public interface IMessageHandler<TMessage> : IMessageHandler
        where TMessage : IMessage
    {
        void Handle(TMessage command);
    }

    public interface IMessageHandler
    {
    }
}
