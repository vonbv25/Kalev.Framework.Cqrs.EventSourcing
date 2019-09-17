using Kalev.Framework.Cqrs.EventSourcing.EventDrivers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kalev.Framework.Cqrs.EventSourcing.Test.MockObjects.Events
{
    public class FlightAdded : IEvent
    {
        public Guid Guid => Guid.NewGuid();
    }
}
