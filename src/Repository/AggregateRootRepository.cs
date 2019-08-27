using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kalev.Framework.Cqrs.EventSourcing.Domain;
using Kalev.Framework.Cqrs.EventSourcing.EventDrivers;
using Kalev.Framework.Cqrs.EventSourcing.Utilities;

namespace Kalev.Framework.Cqrs.EventSourcing.Repository {
    public class AggregateRootRepository<T> : IEventRepository<T> where T : IAggregateRoot, new() 
    {
        private readonly IEventStore _eventStore;
        public AggregateRootRepository (IEventStore eventStore) 
        {
            _eventStore = eventStore;
        }
        public T Find(Guid aggregateRootId)
        {           
           var events = _eventStore.Find(aggregateRootId);

           if(events == null)
           {
               return default(T);
           }

           T aggregateRoot = new T();
           
           aggregateRoot.LoadFromHistory(events);

           return aggregateRoot;
        }
        public async Task<T> FindAsync(Guid aggregateRootId)
        {
            Task<T> findAggregateRoot = Task.Run<T> ( () => { return Find(aggregateRootId); }  );

            return await findAggregateRoot;
        }
        public T Save (T aggregateRoot) {
            
            foreach(var @event in aggregateRoot.AllEvents)
            {
                _eventStore.Save(@event);
            }
            
            aggregateRoot.ConfirmChanges();

            return aggregateRoot;

        }
        public async Task<T> SaveAsync (T aggregateRoot) 
        {
            Task<T> save = Task.Run<T> ( () => 
            {
                foreach(var @event in aggregateRoot.AllEvents)
                {
                    _eventStore.Save(@event);
                }

                aggregateRoot.ConfirmChanges();

                return aggregateRoot;
            }
            );

            return await save;
        }
    }
}