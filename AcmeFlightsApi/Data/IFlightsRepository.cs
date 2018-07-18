using AcmeFlightsApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcmeFlightsApi.Data
{
    public interface IFlightsRepository
    {
        List<Flights> GetAllFlights();
        Flights GetFlightById(int flightId);
        List<FlightsSchedule> GetSchedulesByFlight(int flightId);
        BookingInfo GetBookingById(int bookingId);
        List<FlightsSchedule> GetFlightSchedule(FlightQuery flightQuery);
        bool AddBooking(BookingInfo bookingInfo);
    }
}
