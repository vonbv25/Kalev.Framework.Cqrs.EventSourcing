using Kalev.Framework.DomainDriven.SeedWork.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Kalev.Framework.DomainDriven.SeedWork.Repository
{
    public interface IEventRepository<T> where T : IAggregateRoot
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