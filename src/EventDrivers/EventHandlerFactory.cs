using System;
using System.Collections.Generic;
using System.Text;

namespace Kalev.Framework.Cqrs.EventSourcing.EventDrivers
{
    public interface IEventHandlerFactory
    {
        void Register<TEventStream>(IEventHandler<TEventStream> eventHandler) where TEventStream : EventStream;

        List<IEventHandler<TEventStream>> Resolved<TEventStream>() where TEventStream : EventStream;
    }

    public class EventHandlerFactory : IEventHandlerFactory
    {
        private Dictionary<Type, List<IEventHandlerBase>> registeredEventHandlers;

        public EventHandlerFactory()
        {
            registeredEventHandlers = new Dictionary<Type, List<IEventHandlerBase>>();
        }
        public void Register<TEventStream>(IEventHandler<TEventStream> eventHandler) where TEventStream : EventStream
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

        public List<IEventHandler<TEventStream>> Resolved<TEventStream>() where TEventStream : EventStream
        {            
            var key = typeof(TEventStream);

            if (registeredEventHandlers.Count == 0 || !registeredEventHandlers.ContainsKey(key))
            {
                throw new KeyNotFoundException($"Event handlers for {key.Name} not found");
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
