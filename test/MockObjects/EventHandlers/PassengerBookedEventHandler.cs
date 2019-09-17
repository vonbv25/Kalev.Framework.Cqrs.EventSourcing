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
    public class PassengerBookedEventHandler : IEventHandler<PassengerBooked>
    {
        public async Task NotifyAsync(PassengerBooked eventStream)
        {
            Assert.True(eventStream != null, "Passegner not booked");

            Debug.WriteLine($"{eventStream.GetType().Name} Event Sent out");
        }
    }
}
