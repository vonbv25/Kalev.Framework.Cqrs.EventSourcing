using Kalev.Framework.Cqrs.EventSourcing.EventDrivers;
using test.MockObjects;
using Xunit;

namespace Kalev_Framework_Cqrs_EventSourcing_Test
{
    public class EventHandlerFactoryTest
    {
        [Fact]
        public void EventHandlersForMockHasBeenAddedEventShouldBeResolved()
        {
            //Given
            var notifyFoo = new NotifyFoo();
            var notifyBar = new NotifyBar();
            var notifyMockOne = new NotifyMockOne();
            //When
            var eventHandlerFactory = new EventHandlerFactory();

            eventHandlerFactory.Register<MockHasBeenAddedEvent>(notifyBar);
            eventHandlerFactory.Register<MockHasBeenAddedEvent>(notifyFoo);
            eventHandlerFactory.Register<MockHasBeenRemovedEvent>(notifyMockOne);
            //Then
            var listOfEventHandlersForMockHasBeenAddedEvent = eventHandlerFactory.Resolved<MockHasBeenAddedEvent>();
            var listOfEventHandlersForMockHasBeenRemovedEvent = eventHandlerFactory.Resolved<MockHasBeenRemovedEvent>();
            
            Assert.IsType<NotifyBar>(listOfEventHandlersForMockHasBeenAddedEvent[0]);
            Assert.IsType<NotifyFoo>(listOfEventHandlersForMockHasBeenAddedEvent[1]);
            Assert.IsType<NotifyMockOne>(listOfEventHandlersForMockHasBeenRemovedEvent[0]);

        }
    }
}