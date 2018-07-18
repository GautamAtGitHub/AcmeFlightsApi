using AcmeFlightsApi.Data;
using AcmeFlightsApi.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AcmeFlightsApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Flights")]
    public class FlightsController : Controller
    {
        private readonly IFlightsRepository _repository;
        public FlightsController(IFlightsRepository repository)
        {
            _repository = repository;
        }
        // GET: api/Flights
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_repository.GetAllFlights());
            }
            catch (Exception ex)
            {
                return BadRequest(ExceptionHelper.ProcessError(ex));
            }
        }

        [HttpGet("{flightId}/Schedule", Name = "Schedule")]
        public IActionResult GetSchedule(int flightId)
        {
            try
            {
                return Ok(_repository.GetSchedulesByFlight(flightId));
            }
            catch (Exception ex)
            {
                return BadRequest(ExceptionHelper.ProcessError(ex));
            }
        }

        [HttpGet("Availability", Name = "Availability")]
        public IActionResult Get([FromQuery]FlightQuery flightQuery)
        {
            try
            {
                var scheduleList = _repository.GetFlightSchedule(flightQuery);
                if (scheduleList != null && scheduleList.Count > 0)
                {
                    //MakeBooking
                    var link = new LinkHelper<List<FlightsSchedule>>(scheduleList);
                    for (int iIndex = 0; iIndex < scheduleList.Count || (iIndex < scheduleList.Count && iIndex < 10); iIndex++) // Restrict max 10 links
                    {
                        link.Links.Add(new Link
                        {
                            Href = Url.Link("MakeBooking", new { scheduleList[iIndex].ScheduleId }),
                            Rel = "post-booking",
                            method = "POST"
                        });
                    }
                    return Ok(link);
                }
                else return NotFound("No flight available");
            }
            catch (Exception ex)
            {
                return BadRequest(ExceptionHelper.ProcessError(ex));
            }

        }

        // GET: api/Flights/5
        [HttpGet("{flightid}", Name = "GetFlights")]
        public IActionResult Get(int flightid)
        {
            try
            {
                return Ok(_repository.GetFlightById(flightid));
            }
            catch (Exception ex)
            {
                return BadRequest(ExceptionHelper.ProcessError(ex));
            }
        }

        [HttpGet("Booking/{id}", Name = "GetBooking")]
        public IActionResult GetBooking(int id)
        {
            try
            {
                return Ok(_repository.GetBookingById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ExceptionHelper.ProcessError(ex));
            }
        }

        // POST: api/Flights
        [HttpPost("Booking/{ScheduleId}", Name = "MakeBooking")]
        public IActionResult Post(int ScheduleId, [FromBody]BookingInfo bookingInfo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bookingInfo.ScheduleId = ScheduleId;
                    var bookingConfirmed = _repository.AddBooking(bookingInfo);
                    if (bookingConfirmed)
                        return Created(new Uri("/flights/booking", UriKind.Relative), bookingInfo);
                    else return BadRequest("Booking not confirmed");
                }
                else return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ExceptionHelper.ProcessError(ex));
            }
        }

    }
}
