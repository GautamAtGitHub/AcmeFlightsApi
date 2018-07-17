using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcmeFlightsApi.Model
{
    public class BookingInfo
    {
        [Key]
        public int BookingId { get; set; }
        public string Name { get; set; }
        public int NoOfPax { get; set; }
        public DateTime BookingDate { get; set; }

        [ForeignKey("FlightsSchedule")]
        public int ScheduleId { get; set; }

    }
}
