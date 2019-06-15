using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kalev.Framework.Cqrs.EventSourcing.Domain;

namespace Kalev.Framework.Cqrs.EventSourcing.Repository
{
    public interface IDocumentRepository<T> where T : IAggregateRoot
    {
        #region IO Operations    
        T Save(T aggregateRoot);
        T Find(Guid aggregateRootId);
        #endregion

        #region Async IO
        Task<T> SaveAsync(T aggregateRoot);
        Task<T> FindAsync(Guid aggregateRootId);
        #endregion
        // Task CommitChanges(T aggregateRoot);
    }
}