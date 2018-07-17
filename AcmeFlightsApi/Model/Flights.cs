using System.ComponentModel.DataAnnotations;

namespace AcmeFlightsApi.Model
{
    public class Flights
    {
        [Key]
        public int FlightId { get; set; }
        public string AirlineName { get; set; }
        public int TotalSeats { get; set; }

        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public string DepartureTime { get; set; }
        public string ArivalTime { get; set; }
        public string Duration { get; set; }

    }

    
}
