using System;
using System.Collections.Generic;
using System.Text;
using Kalev.Framework.Cqrs.EventSourcing.EventDrivers;

namespace Kalev.Framework.Cqrs.EventSourcing.Utilities
{
    public class ConsoleEventLogger : IEventLogger
    {
        public void PreLogEvent(IEvent @event, IEventHandler<IEvent> eventHandler)
        {
            string dateTimeString = DateTime.UtcNow.TimeOfDay.ToString();

            Console.WriteLine($"{dateTimeString} starting {eventHandler.GetType().Name} for this {@event.GetType().Name}");
        }
        public void PostLogEvent(IEvent @event, IEventHandler<IEvent> eventHandler)
        {
            string dateTimeString = DateTime.UtcNow.TimeOfDay.ToString();

            Console.WriteLine($"{dateTimeString} {eventHandler.GetType().Name} for this {@event.GetType().Name} was completed");
        }
        public void LogException(Exception e, ExceptionCategory category, IEvent @event)
        {
            Console.WriteLine($"Something Just happened when handling this {@event.GetType().Name} event. Error {e.Message}");
        }
    }
}
