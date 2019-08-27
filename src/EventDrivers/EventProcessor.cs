using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kalev.Framework.Cqrs.EventSourcing.EventDrivers
{
    public interface IEventProcessor
    {
        Task SendAsync(IEvent domainEvent);

        void Send(IEvent domainEvent);
    }

    public class EventProcessor : IEventProcessor
    {
        private readonly IEventHandlerFactory _eventHandlerFactory;
        
        public EventProcessor(IEventHandlerFactory eventHandlerFactory)
        {
            _eventHandlerFactory = eventHandlerFactory;
        }

        public void Send(IEvent domainEvent)
        {
            IEnumerable<IEventHandler<IEvent>> eventHandlers = _eventHandlerFactory.Resolved<IEvent>();

            if (eventHandlers!= null){
                foreach(var eventHandler in eventHandlers)
                {
                    eventHandler.NotifyAsync(domainEvent).Wait();
                }
            }

        }
        public async Task SendAsync(IEvent domainEvent)
        {
            IEnumerable<IEventHandler<IEvent>> eventHandlers = _eventHandlerFactory.Resolved<IEvent>();

            if (eventHandlers!= null){
                foreach(var eventHandler in eventHandlers)
                {
                    await eventHandler.NotifyAsync(domainEvent);
                }
            }

        }
    }
}