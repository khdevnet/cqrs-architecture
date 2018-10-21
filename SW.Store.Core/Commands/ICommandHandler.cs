using MediatR;

namespace SW.Store.Core.Commands
{
    public interface ICommandHandler<in T> : IRequestHandler<T>
        where T : ICommand
    {
    }
}
