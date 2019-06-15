using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kalev.Framework.Cqrs.EventSourcing.CommandDrivers
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<TCommand>(TCommand command) where TCommand : ICommand;        
        Task<TResponse> DispatchAsync<TCommand, TResponse>(TCommand command) where TCommand : ICommand<TResponse>;
    }
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly ICommandHandlerFactory factory;
        public CommandDispatcher(Func<ICommandHandlerFactory> factoryFunc)
        {
            factory = factoryFunc.Invoke();
        }

        public CommandDispatcher(ICommandHandlerFactory factory)
        {
            this.factory = factory;
        }
        
        public async Task<TResponse> DispatchAsync<TCommand, TResponse>(TCommand command) where TCommand : ICommand<TResponse>
        {
            var handler = factory.Resolved<TCommand, TResponse>();

            return await handler.HandleAsync(command);
        }

        public async Task DispatchAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = factory.Resolved<TCommand>();

            await handler.HandleAsync(command);
        }
    }
}
