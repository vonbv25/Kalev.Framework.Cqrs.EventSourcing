using System;
using System.Collections.Generic;
using System.Text;

namespace Kalev.Framework.Cqrs.EventSourcing.CommandDrivers
{
    public interface ICommand
    {
        Guid Guid { get; }        
    }
    public interface ICommand<out TResponse>
    {
        Guid Guid { get; }
    }
}
