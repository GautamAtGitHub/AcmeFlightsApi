using System;

namespace AcmeFlightsApi.Model
{
    public class FlightQuery
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfPax { get; set; }
    }
}
