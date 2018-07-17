using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcmeFlightsApi.Model
{
    public class FlightsSchedule
    {
        [Key]
        public int ScheduleId { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Price { get; set; }
        public int AvailableSeats { get; set; }

        [ForeignKey("Flights")]
        public int FlightId { get; set; }
    }
}
