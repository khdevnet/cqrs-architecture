using SW.Store.Core.Messages;

namespace SW.Store.Core.Queues.ReadStorageSync
{

    public interface IReadStorageSyncMessageHandler<in TMessage> : IMessageHandler<TMessage>, IReadStorageSyncMessageHandler
        where TMessage : IMessage
    {
    }

    public interface IReadStorageSyncMessageHandler : IMessageHandler
    {
    }
}
