using System.Threading.Tasks;

namespace SW.Store.Core.Commands
{
    public interface ICommandBus
    {
        Task Send<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
