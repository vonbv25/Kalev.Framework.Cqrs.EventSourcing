using System;

namespace Kalev.Framework.Cqrs.EventSourcing.EventDrivers
{
    public abstract class EventStream
    {
        private Guid _guid;
        private Guid _aggregateRootId;
        private int _sequenceId;
        private bool _startOfTheSequence;
        private bool _endOfTheSequence;
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
        public void SetSequenceId(int sequenceId)
        {
            _sequenceId = sequenceId;
        }
        public void SetThisEventStreamAsStartOfSequence()
        {
            _startOfTheSequence = true;
        }
        public void SetThisEventStreamAsEndOfSequence()
        {
            _endOfTheSequence = true;
        }
        public bool IsThisTheStartOfSequence()
        {
            return _startOfTheSequence == true;
        }
        public bool IsThisTheEndOfSequence()
        {
            return _endOfTheSequence == true;
        }

    }
}