using Kalev.Framework.Cqrs.EventSourcing.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kalev.Framework.Cqrs.EventSourcing.EventDrivers
{
    public interface IEventProcessor
    {
        Task BroadcastSend(IEvent domainEvent);

        Task SendAsync(IEvent domainEvent);
    }

    public class EventProcessor : IEventProcessor
    {
        private readonly IEventHandlerFactory _eventHandlerFactory;
        private readonly IEventLogger _eventLogger;
        
        public EventProcessor(IEventHandlerFactory eventHandlerFactory)
        {
            _eventHandlerFactory = eventHandlerFactory;
            _eventLogger = eventHandlerFactory.ResolveLogger();
        }

        public async Task SendAsync(IEvent domainEvent)
        {
            IEnumerable<IEventHandler<IEvent>> eventHandlers = _eventHandlerFactory.Resolve<IEvent>();

            if (eventHandlers!= null)
            {
                foreach(var eventHandler in eventHandlers)
                {
                    await eventHandler.NotifyAsync(domainEvent).ConfigureAwait(false);
                }
            }
            {
                var exception = new NoEventHandlersForThisEventException();

                _eventLogger.LogException(exception, ExceptionCategory.Warning, domainEvent);
            }
        }
        public async Task BroadcastSend(IEvent domainEvent)
        {
            IEnumerable<IEventHandler<IEvent>> eventHandlers = _eventHandlerFactory.Resolve<IEvent>();

            List<Task> eventHandlerTask = new List<Task>();

            if (eventHandlers!= null)
            {
                foreach(var eventHandler in eventHandlers)
                {
                    eventHandlerTask.Add(Task.Run( async() =>
                        {
                            try
                            {
                                _eventLogger.PreLogEvent(domainEvent, eventHandler);

                                await eventHandler.NotifyAsync(domainEvent);

                                _eventLogger.PostLogEvent(domainEvent, eventHandler);
                            }
                            catch (Exception e)
                            {
                                _eventLogger.LogException(e, ExceptionCategory.Critical, domainEvent);
                            }
                        }
                        ));
                }
            }
            else
            {
                var exception = new NoEventHandlersForThisEventException();

                _eventLogger.LogException(exception, ExceptionCategory.Warning, domainEvent);
            }

            await Task.WhenAll(eventHandlerTask).ConfigureAwait(false);
        }
    }
}