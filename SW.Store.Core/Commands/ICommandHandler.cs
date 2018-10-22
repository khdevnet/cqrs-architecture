namespace SW.Store.Core.Commands
{
    public interface ICommandHandler<TCommand> : IMessageHandler<TCommand>
        where TCommand : ICommand
    {
    }
}
