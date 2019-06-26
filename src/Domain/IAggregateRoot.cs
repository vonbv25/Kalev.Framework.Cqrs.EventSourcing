using Kalev.Framework.Cqrs.EventSourcing.EventDrivers;
using System;
using System.Collections.Generic;

namespace Kalev.Framework.Cqrs.EventSourcing.Domain
{
    public interface IAggregateRoot
    {
        IEnumerable<EventStream> AllEvents { get; }
        void RegisterEventHandlers(ExternalEventHandlers externalEventHandlers);
        void LoadFromHistory(IEnumerable<EventStream> eventStreams);
        void ConfirmChanges();
        
    }
}