using System.Collections.Generic;

namespace Kalev.Framework.DomainDriven.SeedWork.Domain
{
    public interface IAggregateRoot
    {
        IEnumerable<EventStream> AllEvents { get; }
        void RegisterEventHandlers(ExternalEventHandlers externalEventHandlers);
        void LoadFromHistory(IEnumerable<EventStream> eventStreams);
        void ConfirmChanges();
        void Commit();


    }
}