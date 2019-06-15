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
        public Task NotifyAsync(MockHasBeenAddedEvent eventStream)
        {
            throw new NotImplementedException();
        }
    }

    public class NotifyBar : IEventHandler<MockHasBeenAddedEvent>
    {
        public Task NotifyAsync(MockHasBeenAddedEvent eventStream)
        {
            throw new NotImplementedException();
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
        public Task NotifyAsync(MockHasBeenRemovedEvent eventStream)
        {
            throw new NotImplementedException();
        }
    }
}