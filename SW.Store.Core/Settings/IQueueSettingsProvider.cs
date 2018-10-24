using SW.Store.Core.Settings.Dto;

namespace SW.Store.Core.Settings
{
    public interface IQueueSettingsProvider
    {
        QueueSettings Get();
    }
}
