using Kalev.Framework.Cqrs.EventSourcing.EventDrivers;
using Kalev.Framework.Cqrs.EventSourcing.Test.MockObjects.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Kalev.Framework.Cqrs.EventSourcing.Test.MockObjects.EventHandlers
{
    public class FlightAddedEventHandler : IEventHandler<FlightAdded>
    {
        public async Task NotifyAsync(FlightAdded eventStream)
        {
            Assert.True(eventStream != null, "Flight Not added");

            Debug.WriteLine($"{eventStream.GetType().Name} Event Sent out");
        }
    }
}
