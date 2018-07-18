using AcmeFlightsApi.Model;
using System.Collections.Generic;

namespace AcmeFlightsApi.Data
{
    public interface IFlightsRepository
    {
        List<Flights> GetAllFlights();
        Flights GetFlightById(int flightId);
        List<FlightsSchedule> GetSchedulesByFlight(int flightId);
        BookingInfo GetBookingById(int bookingId);
        List<FlightsSchedule> GetFlightSchedule(FlightQuery flightQuery);
        int AddBooking(BookingInfo bookingInfo);
    }
}
