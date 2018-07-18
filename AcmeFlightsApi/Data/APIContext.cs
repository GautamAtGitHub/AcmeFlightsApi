using AcmeFlightsApi.Model;
using Microsoft.EntityFrameworkCore;

namespace AcmeFlightsApi.Data
{
    public class APIContext : DbContext
    {
        public APIContext(DbContextOptions<APIContext> options) : base(options)
        { }

        public DbSet<Flights> FlightsItems { get; set; }
        public DbSet<FlightsSchedule> FlightsScheduleItems { get; set; }
        public DbSet<BookingInfo> BookingInfoItems { get; set; }
    }
}
