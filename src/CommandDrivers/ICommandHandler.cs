using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kalev.Framework.Cqrs.EventSourcing.CommandDrivers
{
    public interface ICommandHandlerBase
    {
        
    }
    public interface ICommandHandler<TCommand, TResponse> : ICommandHandlerBase
    where TCommand : ICommand<TResponse>
    {
        Task<TResponse> HandleAsync(TCommand command);
    }

    public interface ICommandHandler<TCommand> : ICommandHandlerBase
    where TCommand : ICommand
    {
         Task HandleAsync(TCommand command);       
    }

}
