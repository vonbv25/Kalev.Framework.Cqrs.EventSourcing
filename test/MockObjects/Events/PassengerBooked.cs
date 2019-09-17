using Kalev.Framework.Cqrs.EventSourcing.EventDrivers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kalev.Framework.Cqrs.EventSourcing.Test.MockObjects.Events
{
    public class PassengerBooked : IEvent
    {
        public Guid Guid => Guid.NewGuid();
        public string Name { get; set; }
        public DateTime DepartureDate { get; set; }
    }
}
