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
                var flights = _repository.GetAllFlights();
                var link = new LinkHelper<List<Flights>>(flights);
                for (int iIndex = 0; iIndex < flights.Count || (iIndex < flights.Count && iIndex < 10); iIndex++) // Restrict max 10 links
                {
                    link.Links.Add(new Link
                    {
                        Href = Url.Link("GetFlights", new { flights[iIndex].FlightId }),
                        Rel = "GET-booking",
                        method = "GET"
                    });
                }
                return Ok(link);
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
                var flight = _repository.GetFlightById(flightid);
                if (flight != null)
                {
                    var link = new LinkHelper<Flights>(flight);
                    link.Links.Add(new Link
                    {
                        Href = Url.Link("Schedule", new { flight.FlightId }),
                        Rel = "GET-schedule",
                        method = "GET"
                    });
                    return Ok(link);
                }
                else return NotFound("No flight available");
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
                var scheduleList = _repository.GetSchedulesByFlight(flightId);
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



        [HttpGet("Booking/{bookingId}", Name = "GetBooking")]
        public IActionResult GetBooking(int bookingId)
        {
            try
            {
                var booking = _repository.GetBookingById(bookingId);
                if (booking != null)
                {
                    return Ok(booking);
                }
                else return NotFound("No booking available");

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
                    if (bookingConfirmed > 0)
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
