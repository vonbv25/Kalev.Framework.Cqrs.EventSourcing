using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kalev.Framework.Cqrs.EventSourcing.CommandDrivers
{
    public interface ICommandHandlerFactory
    {
        void Register<TCommand, IResponse>(ICommandHandler<TCommand, IResponse> Handler) where TCommand : ICommand<IResponse>;
        void Register<TCommand>(ICommandHandler<TCommand> Handler) where TCommand : ICommand;
        ICommandHandler<TCommand, IResponse> Resolved<TCommand, IResponse>() where TCommand : ICommand<IResponse>;
        ICommandHandler<TCommand> Resolved<TCommand>() where TCommand : ICommand;
    }

    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        private Dictionary<Type, ICommandHandlerBase> commandHandlersWithResponse;
        private Dictionary<Type, ICommandHandlerBase> commandHandlers;        
        public CommandHandlerFactory()
        {
            commandHandlersWithResponse = new Dictionary<Type, ICommandHandlerBase>();
            commandHandlers             = new Dictionary<Type, ICommandHandlerBase>();
        }

        public void Register<TCommand, IResponse>(ICommandHandler<TCommand, IResponse> Handler) where TCommand : ICommand<IResponse>
        {
            var key = typeof(TCommand);

            //does key exist? then replace the existing value that is map to the key
            if(commandHandlersWithResponse.ContainsKey(key))
            {
                commandHandlersWithResponse[key] = Handler;
            }
            else 
            {
                commandHandlersWithResponse.Add(key, Handler);
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
        public ICommandHandler<TCommand, IResponse> Resolved<TCommand, IResponse>() where TCommand : ICommand<IResponse>
        {
            var key = typeof(TCommand);

            if(commandHandlersWithResponse.Count == 0 || !commandHandlersWithResponse.ContainsKey(key))
            {
                throw new KeyNotFoundException($"Command Handler for this command : {key.Name} is not registered");
            }

            return commandHandlersWithResponse[key] as ICommandHandler<TCommand, IResponse>;
        }
        public ICommandHandler<TCommand> Resolved<TCommand>() where TCommand : ICommand
        {
            var key = typeof(TCommand);

            if(commandHandlers.Count == 0 || !commandHandlers.ContainsKey(key))
            {
                throw new KeyNotFoundException($"Command Handler for this command : {key.Name} is not registered");
            }
            return commandHandlers[key] as ICommandHandler<TCommand>;
        }        
    }
}
