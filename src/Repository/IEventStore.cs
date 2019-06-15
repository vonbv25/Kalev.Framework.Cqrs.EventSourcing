using Kalev.Framework.Cqrs.EventSourcing.EventDrivers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kalev.Framework.Cqrs.EventSourcing.Repository
{
    public interface IEventStore
    {
        #region Async Methods
        Task<int> SaveAsync(EventStream eventStream);
        Task<List<EventStream>> FindAsync(Guid aggregateRootId);
        Task<IEnumerable<List<EventStream>>> FindAsync(Func<List<EventStream>, bool> predicate);
        #endregion

        int Save(EventStream eventStream);
        List<EventStream> Find(Guid aggregateRootId);
        IEnumerable<List<EventStream>> Find(Func<List<EventStream>, bool> predicate);


    }
}