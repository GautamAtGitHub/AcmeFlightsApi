using System;

namespace AcmeFlightsApi.Data
{
    public class FlightQuery
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfPax { get; set; }
    }
}
