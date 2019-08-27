using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kalev.Framework.Cqrs.EventSourcing.CommandDrivers
{
    public interface ICommandHandlerFactory
    {
        void Register<TCommand>(ICommandHandler<TCommand> Handler) where TCommand : ICommand;
        ICommandHandler<TCommand> Resolve<TCommand>() where TCommand : ICommand;
        void Register<TCommand, TDomainState>(ICommandHandler<TCommand, TDomainState> Handler) where TCommand : ICommand<TDomainState>;
        ICommandHandler<TCommand, TDomainState> Resolve<TCommand, TDomainState>() where TCommand : ICommand<TDomainState>;
        ICommandDispatcher BuildCommandDispatcher();
    }

    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        private Dictionary<Type, ICommandHandlerBase> commandHandlersThatReturnsDomainState;
        private Dictionary<Type, ICommandHandlerBase> commandHandlers;        
        public CommandHandlerFactory()
        {
            commandHandlersThatReturnsDomainState   = new Dictionary<Type, ICommandHandlerBase>();
            commandHandlers                         = new Dictionary<Type, ICommandHandlerBase>();
        }
        public void Register<TCommand, TDomainState>(ICommandHandler<TCommand, TDomainState> Handler) where TCommand : ICommand<TDomainState>
        {
            var key = typeof(TCommand);

            //does key exist? then replace the existing value that is map to the key
            if(commandHandlersThatReturnsDomainState.ContainsKey(key))
            {
                commandHandlersThatReturnsDomainState[key] = Handler;
            }
            else 
            {
                commandHandlersThatReturnsDomainState.Add(key, Handler);
            }
        }
        public void Register<TCommand>(ICommandHandler<TCommand> Handler) where TCommand : ICommand
        {
            var key = typeof(TCommand);

            //does key exist? then replace the existing value that is map to the key
            if(commandHandlers.ContainsKey(key))
            {
                commandHandlers[key] = Handler;
            }
            else 
            {
                commandHandlers.Add(key, Handler);
            }
        }
        public ICommandHandler<TCommand, TDomainState> Resolve<TCommand, TDomainState>() where TCommand : ICommand<TDomainState>
        {
            var key = typeof(TCommand);

            if(commandHandlersThatReturnsDomainState.Count == 0 || !commandHandlersThatReturnsDomainState.ContainsKey(key))
            {
                throw new KeyNotFoundException($"Command Handler for this command : {key.Name} is not registered");
            }

            return commandHandlersThatReturnsDomainState[key] as ICommandHandler<TCommand, TDomainState>;
        }
        public ICommandHandler<TCommand> Resolve<TCommand>() where TCommand : ICommand
        {
            var key = typeof(TCommand);

            if(commandHandlers.Count == 0 || !commandHandlers.ContainsKey(key))
            {
                throw new KeyNotFoundException($"Command Handler for this command : {key.Name} is not registered");
            }
            return commandHandlers[key] as ICommandHandler<TCommand>;
        }

        public ICommandDispatcher BuildCommandDispatcher()
        {
            return new CommandDispatcher(this);
        }

    }
}
