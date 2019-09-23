using Kalev.Framework.Cqrs.EventSourcing.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kalev.Framework.Cqrs.EventSourcing.EventDrivers
{
    public interface IEventProcessor
    {
        Task ParallelNotify(IEvent domainEvent);

        Task Notify(IEvent domainEvent);
    }

    public class EventProcessor : IEventProcessor
    {
        private readonly IEventHandlerFactory _eventHandlerFactory;

        public EventProcessor(IEventHandlerFactory eventHandlerFactory)
        {
            _eventHandlerFactory = eventHandlerFactory;
        }

        public async Task Notify(IEvent domainEvent)
        {
            IEnumerable<IEventHandler<IEvent>> eventHandlers = _eventHandlerFactory.Resolve<IEvent>();

            foreach (var eventHandler in eventHandlers)
            {
                await eventHandler.NotifyAsync(domainEvent);
            }
        }
        public async Task ParallelNotify(IEvent domainEvent)
        {
            IEnumerable<IEventHandler<IEvent>> eventHandlers = _eventHandlerFactory.Resolve<IEvent>();

            List<Task> eventHandlerTask = new List<Task>();

            if (eventHandlers!= null)
            {
                foreach(var eventHandler in eventHandlers)
                {
                    eventHandlerTask.Add(Task.Run( async() =>
                        {
                            await eventHandler.NotifyAsync(domainEvent);
                        }
                        ));
                }
            }

            await Task.WhenAll(eventHandlerTask);
        }
    }
}