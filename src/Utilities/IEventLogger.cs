using Kalev.Framework.Cqrs.EventSourcing.EventDrivers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kalev.Framework.Cqrs.EventSourcing.Utilities
{
    public interface IEventLogger
    {
        void PreLogEvent(IEvent @event, IEventHandler<IEvent> eventHandler);
        void PostLogEvent(IEvent @event, IEventHandler<IEvent> eventHandler);
        void LogException(Exception e, ExceptionCategory category, IEvent @event);
    }
}
