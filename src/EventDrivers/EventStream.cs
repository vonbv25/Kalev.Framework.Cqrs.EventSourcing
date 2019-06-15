using System;

namespace Kalev.Framework.Cqrs.EventSourcing.EventDrivers
{
    public abstract class EventStream
    {
        private Guid _guid;
        private Guid _aggregateRootId;
        private DateTime _dateTimeCreated;
        public EventStream(Guid aggregateRootId)
        {          
            _aggregateRootId    = aggregateRootId;
            _guid               = Guid.NewGuid();
            _dateTimeCreated    = DateTime.UtcNow;
        }

        public DateTime DateTimeCreated => _dateTimeCreated;
        public Guid Guid => _guid;
        public Guid AggregateRootId => _aggregateRootId;
    }
}