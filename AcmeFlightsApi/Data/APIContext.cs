using AcmeFlightsApi.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AcmeFlightsApi.Data
{
    public class APIContext : DbContext
    {
        public APIContext(DbContextOptions<APIContext> options) : base(options)
        {

        }

        public DbSet<Flights> FlightsItems { get; set; }
        public DbSet<FlightsSchedule> FlightsScheduleItems { get; set; }
        public DbSet<BookingInfo> BookingInfoItems { get; set; }

        public List<Flights> GetAllFlights()
        {
            return this.FlightsItems.ToList();
        }

        public List<FlightsSchedule> GetSchedules(int flightId)
        {
            return this.FlightsScheduleItems.Where(x => x.FlightId == flightId).ToList();
        }

        public Flights GetFlight(int flightId)
        {
            return this.FlightsItems.Where(x => x.FlightId == flightId).FirstOrDefault();
        }

        public BookingInfo GetBooking(int bookingId)
        {
            return this.BookingInfoItems.Where(x => x.BookingId == bookingId).FirstOrDefault();
        }

        public List<FlightsSchedule> FlightAvailability(FlightQuery flightQuery)
        {
            return this.FlightsScheduleItems.Where(x => x.DepartureDate >= flightQuery.StartDate && x.DepartureDate <= flightQuery.EndDate && x.AvailableSeats >= flightQuery.NumberOfPax).ToList();
        }

        public bool Book(BookingInfo bookingInfo)
        {
            var schedule = this.FlightsScheduleItems.Where(x => x.ScheduleId == bookingInfo.ScheduleId).FirstOrDefault();
            if (schedule != null && schedule.AvailableSeats >= bookingInfo.NoOfPax)
            {
                schedule.AvailableSeats = schedule.AvailableSeats - bookingInfo.NoOfPax;
                this.FlightsScheduleItems.Update(schedule); // Update the number of seats available
                this.BookingInfoItems.Add(bookingInfo); // add the booking 
                this.SaveChanges(); // Save the booking.
                return true; // Booking confirmed
            }

            return false;
        }
    }
}
