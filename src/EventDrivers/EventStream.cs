using System;

namespace Kalev.Framework.Cqrs.EventSourcing.EventDrivers
{
    public abstract class EventStream
    {
        private Guid _guid;
        private Guid _aggregateRootId;
        private int _sequenceId;
        private DateTime _dateTimeCreated;
        private string _eventName;
        public EventStream(Guid aggregateRootId)
        {          
            _aggregateRootId    = aggregateRootId;
            _guid               = Guid.NewGuid();
            _dateTimeCreated    = DateTime.UtcNow;
            _eventName          = this.GetType().Name;
        }

        public DateTime DateTimeCreated => _dateTimeCreated;
        public Guid Guid => _guid;
        public Guid AggregateRootId => _aggregateRootId;
        public string EventName => _eventName;
        public int SequenceId {get => _sequenceId; set => _sequenceId = value; }
    }
}