using Kalev.Framework.Cqrs.EventSourcing.CommandDrivers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kalev.Framework.Cqrs.EventSourcing.Utilities
{
    public interface ICommandLogger
    {
        void PreLogCommand(ICommand command, ICommandHandler<ICommand> commandHandler);
        void PostLogCommand(ICommand command, ICommandHandler<ICommand> commandHandler);
        void LogException(Exception e, ExceptionCategory category, ICommand command);
    }
}
