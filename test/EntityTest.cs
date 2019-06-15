using System;
using System.Collections.Generic;
using System.Linq;
using Kalev.Framework.Cqrs.EventSourcing.Domain;
using Kalev.Framework.Cqrs.EventSourcing.EventDrivers;
using Kalev.Framework.Cqrs.EventSourcing.Test.MockObjects;
using Xunit;

namespace Kalev_Framework_Cqrs_EventSourcing_Test
{
    public class EntityTest
    {
        [Fact]
        public void EntityCreatedEventShouldBeAdded()
        {
            //When            
            var mockEntity = new MockEntityOne();
            //Then
            Assert.True(mockEntity.AllEvents.Any( eventStream => eventStream.GetType() == typeof(EntityCreated)), "EntityCreated event not added");
        }
        [Fact]
        public void EntityShouldBePersisted()
        {
            //Given
            var mockEntity = new MockEntityOne();            
            //When
            mockEntity.ApplyEventChanges();
            //Then
            Assert.False(mockEntity.IsTransient());
        }
        [Fact]        
        public void FooItemShouldBeAdded()
        {
            //Given
            var mockEntity = new MockEntityOne();            
            mockEntity.AddFoos("foo1");
            mockEntity.AddFoos("foo2");
            //When
            mockEntity.ApplyEventChanges();
            //Then
            Assert.True(mockEntity.Foos.Contains("foo1") && mockEntity.Foos.Contains("foo2"));            
        }
        [Fact]
        public void EntityShouldBeCopied()
        {
            var mockEntity = new MockEntityOne();

            mockEntity.ApplyEventChanges();

            var events = new List<EventStream>(mockEntity.AllEvents);
            
            var copiedMockEntity = new MockEntityOne();

            copiedMockEntity.LoadFromHistory(events);
                          
            Assert.True(copiedMockEntity == mockEntity);
        }
        [Fact]        
        public void CopiedEntityShouldHaveHigherVersionThanTheOriginalEntity()
        {
            var mockEntity = new MockEntityOne();
            mockEntity.AddFoos("foo1");
            mockEntity.AddFoos("foo2");
            mockEntity.SetBar(2);

            mockEntity.ApplyEventChanges();

            var events = new List<EventStream>(mockEntity.AllEvents);
            
            var copiedMockEntity = new MockEntityOne();

            copiedMockEntity.LoadFromHistory(events);

            Assert.True( mockEntity.Version < copiedMockEntity.Version );
        }
        [Fact]   
        public void CopiedEntityAndOriginalEntityShouldHaveTheSameEvents()
        {
            var mockEntity = new MockEntityOne();
            mockEntity.AddFoos("foo1");
            mockEntity.SetBar(2);
            mockEntity.ApplyEventChanges();

            var events = new List<EventStream>(mockEntity.AllEvents);
            
            var copiedMockEntity = new MockEntityOne();
            copiedMockEntity.LoadFromHistory(events);

            Assert.True(copiedMockEntity.AllEvents
            .Any( eventStream => eventStream.GetType() == typeof(FoosAdded)), "FoosAdded event is not in the copiedMockEntity");
        }
    }
}
