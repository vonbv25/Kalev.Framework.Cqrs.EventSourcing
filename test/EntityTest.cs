using System;
using System.Collections.Generic;
using System.Linq;
using Kalev.Framework.Cqrs.EventSourcing.Domain;
using Kalev.Framework.Cqrs.EventSourcing.EventDrivers;
using Kalev.Framework.Cqrs.EventSourcing.Test.MockObjects;
using test.MockObjects;
using Xunit;

namespace Kalev_Framework_Cqrs_EventSourcing_Test
{
    public class EntityTest
    {
        [Fact]
        public void EntityCreatedEventShouldBeAdded()
        {
            //When            
            var mockEntity = MockEntityOne.CreateMockEntity();
            //Then
            Assert.True(mockEntity.AllEvents.Any( eventStream => eventStream.GetType() == typeof(MockEntityOneCreated)), "EntityCreated event not added");
        }
        [Fact]
        public void EntityShouldBePersisted()
        {
            //Given
            var mockEntity = MockEntityOne.CreateMockEntity();            
            //When
            mockEntity.ConfirmChanges();
            //Then
            Assert.False(mockEntity.IsTransient());
        }
        [Fact]        
        public void FooItemShouldBeAdded()
        {
            //Given
            var mockEntity = MockEntityOne.CreateMockEntity();            
            mockEntity.AddFoos("foo1");
            mockEntity.AddFoos("foo2");
            //When
            mockEntity.ConfirmChanges();
            //Then
            Assert.True(mockEntity.Foos.Contains("foo1") && mockEntity.Foos.Contains("foo2"));            
        }
        [Fact]
        public void EntityShouldBeCopied()
        {
            var mockEntity = MockEntityOne.CreateMockEntity();

            mockEntity.ConfirmChanges();

            var events = new List<EventStream>(mockEntity.AllEvents);
            
            var copiedMockEntity = new MockEntityOne();

            copiedMockEntity.LoadFromHistory(events);
                          
            Assert.True(copiedMockEntity == mockEntity);
        }
        [Fact]        
        public void CopiedEntityShouldHaveHigherVersionThanTheOriginalEntity()
        {
            var mockEntity = MockEntityOne.CreateMockEntity();
            mockEntity.AddFoos("foo1");
            mockEntity.AddFoos("foo2");
            mockEntity.SetBar(2);

            mockEntity.ConfirmChanges();

            var events = new List<EventStream>(mockEntity.AllEvents);
            
            var copiedMockEntity = new MockEntityOne();

            copiedMockEntity.LoadFromHistory(events);

            Assert.True( mockEntity.Version < copiedMockEntity.Version );
        }
        [Fact]   
        public void CopiedEntityAndOriginalEntityShouldHaveTheSameEvents()
        {
            var mockEntity = MockEntityOne.CreateMockEntity();
            mockEntity.AddFoos("foo1");
            mockEntity.SetBar(2);
            mockEntity.ConfirmChanges();

            var events = new List<EventStream>(mockEntity.AllEvents);
            
            var copiedMockEntity = MockEntityOne.CreateMockEntity();
            copiedMockEntity.LoadFromHistory(events);

            Assert.True(copiedMockEntity.AllEvents
            .Any( eventStream => eventStream.GetType() == typeof(FoosAdded)), "FoosAdded event is not in the copiedMockEntity");
        }
        [Fact]
        public void EventsShouldBeProcessed()
        {
            //Create event processor
            var factory = new EventHandlerFactory();

            factory.Register<FoosAdded>(new YetAnotherFoosAddedEventHandler());
            factory.Register<FoosAdded>(new FoosAddedEventHandler());
            factory.Register<MockHasBeenRemovedEvent>(new NotifyMockOne());

            var processor = factory.BuildEventProcessor();
                        
            var mockEntity = MockEntityOne.CreateMockEntity();
            mockEntity.RegisterEventHandlers(processor.SendAsync);
            mockEntity.AddFoos("foo1");
            mockEntity.ConfirmChanges();           
        }


    }
}
