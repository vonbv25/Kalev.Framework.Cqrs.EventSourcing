using Kalev.Framework.Cqrs.EventSourcing.EventDrivers;
using Kalev.Framework.Cqrs.EventSourcing.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Kalev.Framework.Cqrs.EventSourcing.Domain
{
    public class EntityCreated : EventStream
    {
        private Guid _entityId;
        public EntityCreated(Guid entityId) : base(entityId)
        {
            _entityId = entityId;
        }
        public Guid EntityId => _entityId;

    }
    public abstract class Entity : IAggregateRoot
    {
        #region Declartions
        private Guid _aggregateRootId;
        private int _version;
        private int? _requestedHashCode;
        private List<EventStream> events;
        private Dictionary<Type, MethodInfo> aggregateRootEventHandlerRegistry;       
        #endregion Declartions
        public Entity()
        {            
            aggregateRootEventHandlerRegistry   = new Dictionary<Type, MethodInfo>();
            events                              = new List<EventStream>();
            _version                            = 0;
            //Retrieve all the methods of this entity that has our marker (methods decorated by AggregateRootEvent)
            //Then register it in our eventhandler registry. All event handler registered will be run after we confirm 
            //all the changes needed for this entity.
            var myEventHandlers = GetType()
                                    .GetRuntimeMethods()
                                    .Where(m => m.GetCustomAttributes(typeof(AggregateRootEventHandlerAttribute), false)
                                    .Count() > 0)
                                    .ToList();
            
            if (myEventHandlers != null)
            {
                myEventHandlers.ForEach( (methodInfo) =>
                {
                    aggregateRootEventHandlerRegistry.Add(methodInfo.GetParameters().First().ParameterType, 
                    methodInfo);
                } );
            }

            Guid entityId = Guid.NewGuid();
            //Create this entity by creating an EntityCreated event
            ApplyThisEvent(new EntityCreated(entityId));
        }
        
        public int Version => _version;

        #region Overridden Properties from IAggregateRoot
        public Guid AggregateRootId  {get => _aggregateRootId; }
        public IEnumerable<EventStream> AllEvents => events;

        #endregion Overridden Properties from IAggregateRoot

        #region Overridden Methods from IAggregateRoot
        public void LoadFromHistory(IEnumerable<EventStream> eventStreams)
        {
            //Confirm the first the changes done in this Aggregate root before loading events from the eventStore
            ConfirmedChanges();

            var lastDomainEvent = eventStreams.Last();
            
            foreach(var domainEvent in eventStreams)
            {
                ApplyThisEvent(domainEvent);
                _version++;
            }

            ApplyEventChanges();
        }
        public void ApplyEventChanges()
        {
            if(events.Count == 0)
            {
                return;
            }
            
            events.First()
                    .SetThisEventStreamAsStartOfSequence();            
            events.Last()
                    .SetThisEventStreamAsEndOfSequence();

            int sequenceId = 0;
            foreach(var @event in events)
            {
                sequenceId++;
                @event.SetSequenceId(sequenceId);
                aggregateRootEventHandlerRegistry[@event.GetType()].Invoke(this, new object[] { @event } );
            }
        }

        public void ConfirmedChanges()
        {
            events.Clear();
        }

        #endregion Overridden Methods from IAggregateRoot

        #region Protected virtual Methods
        protected virtual void ApplyThisEvent(EventStream domainEvent)
        {
            events.Add(domainEvent);
        }

        [AggregateRootEventHandler]
        protected virtual void OnEntityCreated(EntityCreated entityCreated)
        {
            _aggregateRootId = entityCreated.AggregateRootId;
        }
        #endregion Protected virtual Methods

        #region Public Methods
        public bool IsTransient()
        {
            return this.AggregateRootId == default(Guid);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity))
                return false;
            if (Object.ReferenceEquals(this, obj))
                return true;
            if (this.GetType() != obj.GetType())
                return false;
            Entity item = (Entity)obj;
            if (item.IsTransient() || this.IsTransient())
                return false;
            else
                return item.AggregateRootId == this.AggregateRootId;
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = this.AggregateRootId.GetHashCode() ^ 31;
                // XOR for random distribution. See:
                // https://blogs.msdn.microsoft.com/ericlippert/2011/02/28/guidelines-and-rules-for-gethashcode/
                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();
        }
        public static bool operator ==(Entity left, Entity right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null));
            else
                return left.Equals(right);
        }
        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }

        #endregion Public Methods
    }
}