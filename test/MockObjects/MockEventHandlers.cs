using System;
using System.Threading.Tasks;
using Kalev.Framework.Cqrs.EventSourcing.EventDrivers;

namespace test.MockObjects
{
    public class MockHasBeenAddedEvent : EventStream
    {        
        public MockHasBeenAddedEvent(Guid aggregateRootId) : base(aggregateRootId)
        {
        }
    }
    
    public class NotifyFoo : IEventHandler<MockHasBeenAddedEvent>
    {
        public async Task NotifyAsync(MockHasBeenAddedEvent eventStream)
        {          
            return;
        }
    }

    public class NotifyBar : IEventHandler<MockHasBeenAddedEvent>
    {
        public async Task NotifyAsync(MockHasBeenAddedEvent eventStream)
        {          
            return;
        }
    }

    public class MockHasBeenRemovedEvent : EventStream
    {
        public MockHasBeenRemovedEvent(Guid aggregateRootId) : base(aggregateRootId)
        {            
        }
    }

    public class NotifyMockOne : IEventHandler<MockHasBeenRemovedEvent>
    {
        public async Task NotifyAsync(MockHasBeenRemovedEvent eventStream)
        {
            return;
        }
    }
}