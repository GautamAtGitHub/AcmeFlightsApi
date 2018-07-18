using AcmeFlightsApi.Model;
using System.Collections.Generic;
using System.Linq;

namespace AcmeFlightsApi.Data
{
    public class FlightsRepository : IFlightsRepository
    {
        private APIContext _context;

        public FlightsRepository(APIContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all the flights info // This is for test purpose
        /// </summary>
        /// <returns></returns>
        public List<Flights> GetAllFlights()
        {
            return _context.FlightsItems.ToList();
        }

        /// <summary>
        /// Get flight as per flight ID // This is for test purpose
        /// </summary>
        /// <param name="flightId">Unique flight identifier</param>
        /// <returns></returns>
        public Flights GetFlightById(int flightId)
        {
            return _context.FlightsItems.Where(x => x.FlightId == flightId).FirstOrDefault();
        }

        /// <summary>
        /// Get Flight schedule by flight ID // This is for test purpose
        /// </summary>
        /// <param name="flightId">flight ID</param>
        /// <returns></returns>
        public List<FlightsSchedule> GetSchedulesByFlight(int flightId)
        {
            return _context.FlightsScheduleItems.Where(x => x.FlightId == flightId).ToList();
        }

        /// <summary>
        /// Get flight booking by ID
        /// </summary>
        /// <param name="bookingId">Booking ID</param>
        /// <returns></returns>
        public BookingInfo GetBookingById(int bookingId)
        {
            return _context.BookingInfoItems.Where(x => x.BookingId == bookingId).FirstOrDefault();
        }

        /// <summary>
        /// Get flight schedule availability as per Start date, end date and No of pax
        /// </summary>
        /// <param name="flightQuery">Start date, end date and No of pax</param>
        /// <returns></returns>
        public List<FlightsSchedule> GetFlightSchedule(FlightQuery flightQuery)
        {
            return _context.FlightsScheduleItems.Where(x => x.DepartureDate >= flightQuery.StartDate && x.DepartureDate <= flightQuery.EndDate && x.AvailableSeats >= flightQuery.NumberOfPax).ToList();
        }

        /// <summary>
        /// Perform Booking as per booking info
        /// </summary>
        /// <param name="bookingInfo">booking info</param>
        /// <returns>Return booking ID</returns>
        public int AddBooking(BookingInfo bookingInfo)
        {
            var schedule = _context.FlightsScheduleItems.Where(x => x.ScheduleId == bookingInfo.ScheduleId).FirstOrDefault();
            if (schedule != null && schedule.AvailableSeats >= bookingInfo.NoOfPax)
            {
                schedule.AvailableSeats = schedule.AvailableSeats - bookingInfo.NoOfPax;
                _context.FlightsScheduleItems.Update(schedule); // Update the number of seats available
                _context.BookingInfoItems.Add(bookingInfo); // add the booking 
                _context.SaveChanges(); // Save the booking.
                return bookingInfo.BookingId; // Booking confirmed
            }
            return 0;
        }
    }
}
