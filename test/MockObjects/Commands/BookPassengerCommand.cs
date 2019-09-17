using Kalev.Framework.Cqrs.EventSourcing.CommandDrivers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kalev.Framework.Cqrs.EventSourcing.Test.MockObjects.Commands
{
    public class BookPassengerCommand : ICommand<BookingState>
    {
        public Guid Guid => Guid.NewGuid();
    }

    public class BookingState
    {
        public State State { get; set; }
    }

    public enum State
    {
        Confirmed,
        NotConfirmed
    }


}
