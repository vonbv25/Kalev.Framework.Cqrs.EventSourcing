using System;
using System.Collections.Generic;

using Kalev.Framework.Cqrs.EventSourcing.Domain;
using Kalev.Framework.Cqrs.EventSourcing.EventDrivers;
using Kalev.Framework.Cqrs.EventSourcing.Utilities;

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
    public class BarSet : EventStream
    {
        public int Bar;
        public BarSet(int bar, Guid aggregateRootId) : base(aggregateRootId)
        {
            Bar = bar;
        }
    }
    public class MockEntityOne : Entity
    {
        private List<string> foos = new List<string>();
        private int bar;        
        public int Bar => bar;
        public List<string> Foos => foos;
        public void AddFoos(string foo)
        {
            Apply(new FoosAdded(foo, AggregateRootId));
        }
        public void SetBar(int bar)
        {
            Apply(new BarSet(bar, AggregateRootId));
        }
        [AggregateRootEventHandler]
        private void OnAddedFoos(FoosAdded foosAdded)
        {
            foos.Add(foosAdded.Foo);
        }
        [AggregateRootEventHandler]
        private void OnSetBar(BarSet barSet)
        {
            bar = barSet.Bar;
        }
    }
}