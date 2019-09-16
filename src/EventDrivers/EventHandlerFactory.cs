using Kalev.Framework.Cqrs.EventSourcing.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kalev.Framework.Cqrs.EventSourcing.EventDrivers
{
    public interface IEventHandlerFactory
    {
        void Register<TEventStream>(IEventHandler<TEventStream> eventHandler) where TEventStream : class, IEvent;
        void RegisterEventLogger<TEventLogger>(TEventLogger eventLogger) where TEventLogger : class, IEventLogger;
        List<IEventHandler<TEventStream>> Resolve<TEventStream>() where TEventStream : class, IEvent;
        IEventLogger ResolveLogger();
        IEventProcessor BuildEventProcessor();
    }

    public class EventHandlerFactory : IEventHandlerFactory
    {
        private Dictionary<Type, List<IEventHandlerBase>> registeredEventHandlers;
        private IEventLogger eventLogger;

        public EventHandlerFactory()
        {
            registeredEventHandlers = new Dictionary<Type, List<IEventHandlerBase>>();
            eventLogger = new ConsoleEventLogger();
        }

        public IEventProcessor BuildEventProcessor()
        {
            return new EventProcessor(this);
        }

        public void Register<TEventStream>(IEventHandler<TEventStream> eventHandler) where TEventStream : class, IEvent
        {
            var key = typeof(TEventStream);
            
            if(registeredEventHandlers.ContainsKey(key))
            {
                registeredEventHandlers[key].Add(eventHandler);
            }
            else
            {
                List<IEventHandlerBase> listOfEventHandlersToBeRegistered = new List<IEventHandlerBase>();

                listOfEventHandlersToBeRegistered.Add(eventHandler);

                registeredEventHandlers.Add(key, listOfEventHandlersToBeRegistered);
            }
        }

        public void RegisterEventLogger<TEventLogger>(TEventLogger eventLogger) where TEventLogger : class, IEventLogger
        {
            this.eventLogger = eventLogger;
        }


        public IEventLogger ResolveLogger()
        {
            return eventLogger;
        }

        public List<IEventHandler<TEventStream>> Resolve<TEventStream>() where TEventStream : class, IEvent
        {            
            var key = typeof(TEventStream);

            if (registeredEventHandlers.Count == 0 || !registeredEventHandlers.ContainsKey(key))
            {
                //TODO: Find a way to get the name of the derived type
                return null;
            }

            List<IEventHandler<TEventStream>> eventHandlers = new List<IEventHandler<TEventStream>>();

            foreach(var eventHandler in registeredEventHandlers[key])
            {
                eventHandlers.Add(eventHandler as IEventHandler<TEventStream>);
            }

            return eventHandlers;
        }
    }
}
