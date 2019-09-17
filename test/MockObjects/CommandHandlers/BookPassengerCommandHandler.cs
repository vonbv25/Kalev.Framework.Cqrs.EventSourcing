using Kalev.Framework.Cqrs.EventSourcing.CommandDrivers;
using Kalev.Framework.Cqrs.EventSourcing.Test.MockObjects.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Kalev.Framework.Cqrs.EventSourcing.Test.MockObjects.CommandHandlers
{
    public class BookPassengerCommandHandler : ICommandHandler<BookPassengerCommand, BookingState>
    {
        public async Task<BookingState> HandleAsync(BookPassengerCommand command)
        {
            return await Task.Run(() =>
            {

                BookingState bookingState = new BookingState();

                bookingState.State = State.Confirmed;

                Assert.True(bookingState.State == State.Confirmed, "Booking not confirmed");

                Debug.WriteLine($"{command.GetType().Name} handled");

                return bookingState;

            });
        }
    }
}
