﻿using MediatR;
using System.Threading.Tasks;

namespace SW.Store.Core.Commands
{
    internal class CommandBus : ICommandBus
    {
        private readonly IMediator _mediator;

        public CommandBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            return _mediator.Send(command);
        }
    }
}
