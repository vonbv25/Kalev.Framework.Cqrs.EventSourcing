using Kalev.Framework.Cqrs.EventSourcing.QueryDrivers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kalev.Framework.Cqrs.EventSourcing.Test.MockObjects.Request
{
    public class FlightAvailabilityRequest : IRequest<FlightAvailabilityResponse>
    {
        public Guid Guid => Guid.NewGuid();

        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
    }

    public class FlightAvailabilityResponse
    {
        List<Flight> availableFlights = new List<Flight>();
        public FlightAvailabilityResponse(List<Flight> availableFlights)
        {
            this.availableFlights = availableFlights;
        }
        public List<Flight> AvailableFlights { get => availableFlights; }
    }

    public class Flight
    {
        public string CarrierCode { get; set; }
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
    }
}
