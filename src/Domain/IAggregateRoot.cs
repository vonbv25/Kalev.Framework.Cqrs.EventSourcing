using Kalev.Framework.Cqrs.EventSourcing.EventDrivers;
using System;
using System.Collections.Generic;

namespace Kalev.Framework.Cqrs.EventSourcing.Domain
{
    public interface IAggregateRoot
    {
        Guid AggregateRootId { get; }
        IEnumerable<EventStream> AllEvents { get; }
        void LoadFromHistory(IEnumerable<EventStream> eventStreams);
        void ApplyEventChanges();
        void ConfirmedChanges();
    }
}