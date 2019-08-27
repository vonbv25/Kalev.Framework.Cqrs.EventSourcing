using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kalev.Framework.Cqrs.EventSourcing.CommandDrivers
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<TCommand>(TCommand command) where TCommand : ICommand;
    }

    public interface ICommandWithDomainStateDispatcher
    {
        Task<TDomainState> DispatchAsync<TCommand, TDomainState>(TCommand command) where TCommand : ICommand<TDomainState>;
    }
    public class CommandDispatcher : ICommandDispatcher, ICommandWithDomainStateDispatcher
    {
        private readonly ICommandHandlerFactory factory;

        public CommandDispatcher(ICommandHandlerFactory factory)
        {
            this.factory = factory;
        }
        
        public async Task<TDomainState> DispatchAsync<TCommand, TDomainState>(TCommand command) where TCommand : ICommand<TDomainState>
        {
            var handler = factory.Resolve<TCommand, TDomainState>();

            return await handler.HandleAsync(command);
        }

        public async Task DispatchAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = factory.Resolve<TCommand>();

            await handler.HandleAsync(command);
        }
    }
}
