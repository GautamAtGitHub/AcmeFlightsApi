using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcmeFlightsApi.Model
{
    public class BookingInfo
    {
        [Key]
        public int BookingId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0, 6)]
        public int NoOfPax { get; set; }
        public DateTime BookingDate { get; set; }

        [Required]
        [ForeignKey("FlightsSchedule")]
        public int ScheduleId { get; set; }
    }
}
