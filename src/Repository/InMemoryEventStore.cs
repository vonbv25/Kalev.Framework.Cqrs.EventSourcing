using Kalev.Framework.Cqrs.EventSourcing.EventDrivers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kalev.Framework.Cqrs.EventSourcing.Repository
{
    public class InMemoryEventStore : IEventStore
    {
        private ConcurrentDictionary<Guid, List<EventStream>> _inMemoryEventStreamStorage;

        public InMemoryEventStore()
        {
            _inMemoryEventStreamStorage = new ConcurrentDictionary<Guid, List<EventStream>>();
        }

        public List<EventStream> Find(Guid aggregateRootId)
        {
            bool isFound = _inMemoryEventStreamStorage.TryGetValue(aggregateRootId, out List<EventStream> eventStream);
            
            if(!isFound) return null;

            return eventStream;
        }

        public IEnumerable<List<EventStream>> Find(Func<List<EventStream>, bool> predicate)
        {
            return _inMemoryEventStreamStorage.Values.Where(predicate);
        }

        public async Task<List<EventStream>> FindAsync(Guid aggregateRootId)
        {
            Task<List<EventStream>> findValueTask = Task.Run<List<EventStream>> ( () => { return Find(aggregateRootId); } );

            return await findValueTask;
        }

        public async Task<IEnumerable<List<EventStream>>> FindAsync(Func<List<EventStream>, bool> predicate)
        {
            Task<IEnumerable<List<EventStream>>> findValueTask = Task.Run<IEnumerable<List<EventStream>>> ( () => 
            { 
                return Find(predicate); 
            } );

            return await findValueTask;
        }

        public int Save(EventStream eventStream)
        {

            //check if we already have this aggregate id in our event store

            Guid key = eventStream.AggregateRootId;

            var savedEvent = _inMemoryEventStreamStorage.AddOrUpdate( key, new List<EventStream>() { eventStream }, 
                (_key, eventStreamList) => 
                {
                    
                    eventStreamList.Add(eventStream);

                    return eventStreamList;
                });

            return 0;

        }

        public async Task<int> SaveAsync(EventStream eventStream)
        {
            Task<int> saveValueTask = Task.Run<int> ( () => { return Save(eventStream); } );

            return await saveValueTask;
            
        }
    }
}
