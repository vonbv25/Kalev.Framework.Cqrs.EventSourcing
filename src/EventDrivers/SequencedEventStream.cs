using System;

namespace Kalev.Framework.Cqrs.EventSourcing.EventDrivers
{    
    public abstract class SequencedEventStream : EventStream
    {
        private int _sequenceId;
        private bool _startOfTheSequence;
        private bool _endOfTheSequence;
        public SequencedEventStream(Guid aggregateRootId) : base(aggregateRootId)
        {
            _startOfTheSequence = false;
            _endOfTheSequence   = false;
        }
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