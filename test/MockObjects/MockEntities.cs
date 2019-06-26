using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kalev.Framework.Cqrs.EventSourcing.Domain;
using Kalev.Framework.Cqrs.EventSourcing.EventDrivers;
using Kalev.Framework.Cqrs.EventSourcing.Utilities;
using Xunit;

namespace Kalev.Framework.Cqrs.EventSourcing.Test.MockObjects
{
    public class FoosAdded : EventStream
    {
        public string Foo;
        public FoosAdded(string foo, Guid aggregateRootId) : base(aggregateRootId)
        {
            Foo = foo;
        }
    }

    public class FoosAddedEventHandler : IEventHandler<FoosAdded>
    {
        public async Task NotifyAsync(FoosAdded eventStream)
        {
            Assert.True(true);
            return;
        }
    }

    public class YetAnotherFoosAddedEventHandler : IEventHandler<FoosAdded>
    {
        public async Task NotifyAsync(FoosAdded eventStream)
        {
            Assert.True(true);
            return;
        }
    }
    public class BarSet : EventStream
    {
        public int Bar;
        public BarSet(int bar, Guid aggregateRootId) : base(aggregateRootId)
        {
            Bar = bar;
        }
    }

    public class MockEntityOneCreated : EventStream
    {        
        public MockEntityOneCreated(Guid aggregateRootId) : base(aggregateRootId)
        {
        }
    }
    public class MockEntityOne : AggregateEntity
    {
        private List<string> foos = new List<string>();
        private int bar;        
        public int Bar => bar;
        public List<string> Foos => foos;
        
        public static MockEntityOne CreateMockEntity()
        {
            Guid guid = Guid.NewGuid();

            var mockEntityOne = new MockEntityOne();

            mockEntityOne.Apply(new MockEntityOneCreated(guid));

            return mockEntityOne;
        }

        public void AddFoos(string foo)
        {
            Apply(new FoosAdded(foo, AggregateRootId));
        }
        public void SetBar(int bar)
        {
            Apply(new BarSet(bar, AggregateRootId));
        }

        [Register]        
        public void MockedEntityCreated(MockEntityOneCreated created)
        {
            this.AggregateRootId = created.AggregateRootId;
        }

        [Register]
        private void OnAddedFoos(FoosAdded foosAdded)
        {
            foos.Add(foosAdded.Foo);
        }
        [Register]
        private void OnSetBar(BarSet barSet)
        {
            bar = barSet.Bar;
        }
    }
}