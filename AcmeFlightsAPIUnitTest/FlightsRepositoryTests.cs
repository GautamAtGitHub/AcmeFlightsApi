using AcmeFlightsApi.Data;
using AcmeFlightsApi.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AcmeFlightsAPIUnitTest
{
    [TestClass]
    public class FlightsRepositoryTests
    {
        APIContext _moqContext;

        public FlightsRepositoryTests()
        {
            InitContext();
        }

        /// <summary>
        /// Initialise context for testing. We have used in memory database for testing.
        /// We can use Moq library as well for testing.
        /// </summary>
        public void InitContext()
        {
            var builder = new DbContextOptionsBuilder<APIContext>().UseInMemoryDatabase("APIMockDB");
            var context = new APIContext(builder.Options);

            // Seed the database.
            context.FlightsItems.Add(new Flights
            {
                FlightId = 101,
                AirlineName = "ACME",
                TotalSeats = 6,
                FromLocation = "Brisbane",
                ToLocation = "Gold Coast",
                DepartureTime = "8.00.00 AM",
                ArivalTime = "9.00.00 AM",
                Duration = "1 Hour"
            });

            context.FlightsScheduleItems.Add(new FlightsSchedule
            {
                ScheduleId = 1001,
                AvailableSeats = 6,
                DepartureDate = DateTime.Today,
                Price = "150 $",
                FlightId = 101
            });


            context.SaveChanges();
            _moqContext = context;
        }

        [TestMethod]
        public void GetAllFlightsTest()
        {
            var moqFlightsRepository = new FlightsRepository(_moqContext);

            var flights = moqFlightsRepository.GetAllFlights();

            // Asset
            Assert.IsNotNull(flights);
            //Ensure there is only 1 flight.
            Assert.AreEqual(1, flights.Count);
            //Ensure flight id is same as set in moq object
            Assert.AreEqual(101, flights[0].FlightId);
        }

        [TestMethod]
        public void AddBookingTest()
        {
            var moqFlightsRepository = new FlightsRepository(_moqContext);
            var newBooking = new BookingInfo() { Name = "Gautam", NoOfPax = 2, ScheduleId = 1001 };
            var added = moqFlightsRepository.AddBooking(newBooking);

            //Ensure that record is added
            Assert.IsTrue(added > 0, "Booking is not added");

            var addedBooking = moqFlightsRepository.GetBookingById(added);

            //Ensure that added object is same as moq object
            Assert.AreEqual(newBooking.Name, addedBooking.Name);
            Assert.AreEqual(newBooking.NoOfPax, addedBooking.NoOfPax);
            Assert.AreEqual(newBooking.ScheduleId, addedBooking.ScheduleId);
        }

        [TestMethod]
        public void AddBookingFailTest()
        {
            var moqFlightsRepository = new FlightsRepository(_moqContext);
            var newBooking = new BookingInfo() { Name = "Shweta", NoOfPax = 10, ScheduleId = 1001 };
            var added = moqFlightsRepository.AddBooking(newBooking);
            //Here booking will fail since No of pax is more than available seat capacity
            //Ensure that record is added
            Assert.IsTrue(added == 0, "Booking is added");
        }

        [TestMethod]
        public void GetScheduleByFlightTest()
        {
            var moqFlightsRepository = new FlightsRepository(_moqContext);
            var actualSchedule = moqFlightsRepository.GetSchedulesByFlight(101);

            // Asset
            Assert.IsNotNull(actualSchedule);
            //Ensure there is only 1 schedule.
            Assert.AreEqual(1, actualSchedule.Count);
            //Ensure Schedule id is same as set in moq object
            Assert.AreEqual(1001, actualSchedule[0].ScheduleId);
        }

        [TestMethod]
        public void GetScheduleByFlightFailTest()
        {
            var moqFlightsRepository = new FlightsRepository(_moqContext);
            var actualSchedule = moqFlightsRepository.GetSchedulesByFlight(500);

            // Asset // count should be 0 since flight id is invalid
            Assert.AreEqual(0, actualSchedule.Count);
        }

        [TestMethod]
        public void GetFlightsScheduleTest()
        {
            var moqFlightsRepository = new FlightsRepository(_moqContext);
            var actualSchedule = moqFlightsRepository.GetFlightSchedule(new FlightQuery() { StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(1), NumberOfPax = 2 });

            Assert.IsNotNull(actualSchedule);
            Assert.AreEqual(1, actualSchedule.Count);
            Assert.AreEqual(1001, actualSchedule[0].ScheduleId);
        }

        [TestMethod]
        public void GetFlightsScheduleFailTest()
        {
            var moqFlightsRepository = new FlightsRepository(_moqContext);
            var actualSchedule = moqFlightsRepository.GetFlightSchedule(new FlightQuery() { StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(1), NumberOfPax = 30 });
            //Assert count should be 0 since number of pax is greater than 30
            Assert.AreEqual(0, actualSchedule.Count);
        }
    }
}
