using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kalev.Framework.Cqrs.EventSourcing.CommandDrivers
{
    public interface ICommandDispatcherBase
    {

    }
    public interface ICommandDispatcher : ICommandDispatcherBase
    {
        Task DispatchAsync<TCommand>(TCommand command) where TCommand : ICommand;       
    }

    public interface ICommandWithDomainStateDispatcher : ICommandDispatcherBase
    {
        Task<TDomainState> DispatchAsync<TCommand, TDomainState>(TCommand command) where TCommand : ICommand<TDomainState>;
    }
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly ICommandHandlerFactory factory;
        private Action<ICommand> preHandler;
        private Action<ICommand> postHandler;

        public CommandDispatcher(ICommandHandlerFactory factory)
        {
            this.factory = factory;
        }

        public void AddPostHandler(Action<ICommand> postProcessHandler)
        {
            postHandler = postProcessHandler;
        }

        public void AddPreHandler(Action<ICommand> preProcessHandler)
        {
            preHandler = preProcessHandler;
        }
       
        public async Task DispatchAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = factory.Resolve<TCommand>();

            await Task.Run(async () =>
           {

               if (preHandler != null)
               {
                   preHandler.Invoke(command);
               }

               await handler.HandleAsync(command);

               if (postHandler != null)
               {
                   postHandler.Invoke(command);
               }

           }).ConfigureAwait(false);

        }
    }

    public class CommandWithDomainStateDispatcher : ICommandWithDomainStateDispatcher
    {
        private readonly ICommandHandlerFactory factory;
        private Action<ICommand> preHandler;
        private Action<ICommand> postHandler;
        public CommandWithDomainStateDispatcher(ICommandHandlerFactory factory)
        {
            this.factory = factory;
        }

        public void AddPostHandler(Action<ICommand> postProcessHandler)
        {
            postHandler = postProcessHandler;
        }

        public void AddPreHandler(Action<ICommand> preProcessHandler)
        {
            preHandler = preProcessHandler;
        }

        public async Task<TDomainState> DispatchAsync<TCommand, TDomainState>(TCommand command) 
            where TCommand : ICommand<TDomainState>
        {
            var handler = factory.Resolve<TCommand, TDomainState>();

            return await Task.Run(async () =>
            {
                if (preHandler != null)
                {
                    preHandler.Invoke(command);
                }

                var domainState = await handler.HandleAsync(command);

                if (postHandler != null)
                {
                    postHandler.Invoke(command);
                }

                return domainState;

            }).ConfigureAwait(false);
        }
    }
}
