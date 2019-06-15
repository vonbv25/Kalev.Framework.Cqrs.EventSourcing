using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kalev.Framework.Cqrs.EventSourcing.EventDrivers
{
    public interface IEventProcessor
    {
        Task SendAsync(EventStream domainEvent);

        void Send(EventStream domainEvent);
    }

    public class EventProcessor : IEventProcessor
    {
        private readonly IEventHandlerFactory _eventHandlerFactory;

        public EventProcessor(Func<IEventHandlerFactory> eventFactoryFunc)
        {
            _eventHandlerFactory = eventFactoryFunc.Invoke();
        }
        public EventProcessor(IEventHandlerFactory eventHandlerFactory)
        {
            _eventHandlerFactory = eventHandlerFactory;
        }

        public void Send(EventStream domainEvent)
        {
            IEnumerable<IEventHandler<EventStream>> eventHandlers = _eventHandlerFactory.Resolved<EventStream>();

            foreach(var eventHandler in eventHandlers)
            {
                eventHandler.NotifyAsync(domainEvent).Wait();
            }
        }
        public async Task SendAsync(EventStream domainEvent)
        {
            IEnumerable<IEventHandler<EventStream>> eventHandlers = _eventHandlerFactory.Resolved<EventStream>();

            foreach(var eventHandler in eventHandlers)
            {
                await eventHandler.NotifyAsync(domainEvent);
            }
        }
    }
}