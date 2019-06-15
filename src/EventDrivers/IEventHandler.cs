using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kalev.Framework.Cqrs.EventSourcing.EventDrivers
{
    public interface IEventHandlerBase
    {
        
    }
    public interface IEventHandler<TEventStream> : IEventHandlerBase where TEventStream : EventStream
    {
        Task NotifyAsync(TEventStream eventStream);
    }
}
