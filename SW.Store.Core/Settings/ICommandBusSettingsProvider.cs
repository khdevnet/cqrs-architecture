using SW.Store.Core.Settings.Dto;

namespace SW.Store.Core.Settings
{
    public interface ICommandBusSettingsProvider
    {
        CommandBusSettings Get();
    }
}
