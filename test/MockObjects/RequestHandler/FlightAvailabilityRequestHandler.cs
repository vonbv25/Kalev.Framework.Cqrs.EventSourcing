using Kalev.Framework.Cqrs.EventSourcing.QueryDrivers;
using Kalev.Framework.Cqrs.EventSourcing.Test.MockObjects.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Kalev.Framework.Cqrs.EventSourcing.Test.MockObjects.RequestHandler
{
    public class FlightAvailabilityRequestHandler : IRequestHandler<FlightAvailabilityRequest, FlightAvailabilityResponse>
    {
        public FlightAvailabilityResponse SendRequest(FlightAvailabilityRequest request)
        {

            Assert.True(request != null, "request not recieved");

            Flight flight = new Flight();

            flight.ArrivalDate = DateTime.Now.AddDays(10);
            flight.DepartureDate = DateTime.Now.AddDays(1);
            flight.Origin = "SLC";
            flight.Destination = "DEN";
            flight.FlightNumber = "1234";

            var response = new FlightAvailabilityResponse(new List<Flight> {
                flight
            });

            return response;
        }

        public async Task<FlightAvailabilityResponse> SendRequestAsync(FlightAvailabilityRequest request)
        {
            return await Task.Run(() =>
           {
               return SendRequest(request);
           });
        }
    }
}
