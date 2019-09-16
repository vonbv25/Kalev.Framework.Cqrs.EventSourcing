using Kalev.Framework.Cqrs.EventSourcing.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kalev.Framework.DomainDriven.SeedWork.Domain
{
    public abstract class Entity : IAggregateRoot
    {
        #region Declartions
        private int _version;
        private int? _requestedHashCode;
        private Guid aggregateRootId;
        private ExternalEventHandlers externalEventHandlers;
        private List<EventStream> events;
        private Dictionary<Type, MethodInfo> entityStates;

        #endregion Declartions
        protected Entity()
        {            
            entityStates                        = new Dictionary<Type, MethodInfo>();
            events                              = new List<EventStream>();
            _version                            = 0;
            //Retrieve all the methods of this entity that has our marker (methods decorated by EntityState)
            //Then register it in our eventhandler registry. All event handler registered will be run after we confirm 
            //all the changes needed for this entity.
            var myEventHandlers = GetType()
                                    .GetRuntimeMethods()
                                    .Where(m => m.GetCustomAttributes(typeof(EntityStateAttribute), false)
                                    .Count() > 0)
                                    .ToList();
            
            if (myEventHandlers != null)
            {
                myEventHandlers.ForEach( 
                    (methodInfo) =>
                    {
                        entityStates.Add(methodInfo.GetParameters()
                                                   .First()
                                                   .ParameterType, methodInfo);
                    } );
            }
        }
        public Guid AggregateRootId {get => aggregateRootId; set => aggregateRootId =value; }
        public int Version => _version;

        #region Overridden Properties from IAggregateRoot
        public IEnumerable<EventStream> AllEvents => events;

        #endregion Overridden Properties from IAggregateRoot

        #region Overridden Methods from IAggregateRoot
        public void LoadFromHistory(IEnumerable<EventStream> eventStreams)
        {
            foreach(var domainEvent in eventStreams)
            {
                Apply(domainEvent);
                _version++;
            }

            ConfirmChanges();
        }
        public void RegisterEventHandlers(ExternalEventHandlers externalEventHandlers)
        {
            this.externalEventHandlers += externalEventHandlers;
        }
        public void ConfirmChanges()
        {
            if(events.Count == 0)
            {
                return;
            }

            int sequenceId = 0;

            events.ForEach(async _event =>
            {
                sequenceId++;
                _event.SequenceId = sequenceId;
                //Update the state of the this Entity by applying all Entity States
                entityStates[_event.GetType()].Invoke(this, new object[] { _event } );
                
                if(externalEventHandlers!= null)
                {
                    //Notify all Event Handlers
                    await externalEventHandlers(_event);
                }
            } );
        }

        public void Commit()
        {
            events.Clear();
        }

        #endregion Overridden Methods from IAggregateRoot

        #region Protected virtual Methods
        protected virtual void Apply(EventStream domainEvent)
        {
            events.Add(domainEvent);
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