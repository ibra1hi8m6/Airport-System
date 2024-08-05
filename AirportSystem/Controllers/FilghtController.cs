using AirportSystem.Data;
using AirportSystem.Entity;
using AirportSystem.Exceptions;
using AirportSystem.Forms;
using AirportSystem.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly ApplicationDbContext _context;

        public FlightController(IFlightService flightService, ApplicationDbContext context)
        {
            _flightService = flightService;
            _context = context;
        }
        //[Authorize(Roles = "Admin")]
        [HttpPost("CreateFlight")]
        public async Task<IActionResult> CreateFlight([FromBody] FlightServiceFormModel flightModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var flight = await _flightService.CreateFlightAsync(flightModel);
                var plane = await _context.Planes.FirstOrDefaultAsync(p => p.Id == flight.PlaneId);

                var flightResponse = new FlightResponseModel
                {
                    FlightId = flight.Id,
                    DepartureTime = flight.DepartureTime,
                    ArrivalTime = flight.ArrivalTime,
                    TakeoffLocation = flight.Takeoff_Location,
                    Destination = flight.Destination,
                    PlaneId = flight.PlaneId,
                    PlaneModel = plane?.Plane_model,
                    PlanePayload = plane?.Plane_Payload ?? 0
                };

                return Ok(flightResponse);
            }
            catch (AirportSystemException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Handle unexpected exceptions
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpGet("GetFlightById/{id}")]
        public async Task<IActionResult> GetFlightById(Guid id)
        {
            var flight = await _flightService.GetFlightByIdAsync(id);
            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFlights()
        {
            var flights = await _flightService.GetAllFlightsAsync();
            return Ok(flights);
        }

        [HttpGet("GetFlightsWithDoctors")]
        public async Task<IActionResult> GetFlightsWithDoctors()
        {
            var flights = await _flightService.GetFlightsWithDoctorsAsync();
            return Ok(flights);
        }

       

        [HttpGet("GetFlightsWithDurationGreaterThan/{hours}")]
        public async Task<IActionResult> GetFlightsWithDurationGreaterThan(int hours)
        {
            var duration = TimeSpan.FromHours(hours);
            var flights = await _flightService.GetFlightsByDurationAsync(duration, true);
            return Ok(flights);
        }

        [HttpGet("GetFlightsWithDurationLessThan/{hours}")]
        public async Task<IActionResult> GetFlightsWithDurationLessThan(int hours)
        {
            var duration = TimeSpan.FromHours(hours);
            var flights = await _flightService.GetFlightsByDurationAsync(duration, false);
            return Ok(flights);
        }
        //[Authorize(Roles = "Admin")]
        [HttpPut("UpdateFlight/{id}")]
        public async Task<IActionResult> UpdateFlight(Guid id, [FromBody] FlightServiceFormModel flightModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _flightService.UpdateFlightAsync(id, flightModel);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteFlight/{id}")]
        public async Task<IActionResult> DeleteFlight(Guid id)
        {
            try
            {
                await _flightService.DeleteFlightAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet(" GetFlightsByPilotId/{pilotId}")]
        public async Task<IActionResult> GetFlightsByPilotId(Guid pilotId)
        {
            var flights = await _flightService.GetFlightsByPilotIdAsync(pilotId);
            return Ok(flights);
        }

        [HttpGet("GetFlightsByLocation")]
        public async Task<IActionResult> GetFlightsByLocation(string location, bool isDeparture)
        {
            var flights = await _flightService.GetFlightsByLocationAsync(location, isDeparture);
            return Ok(flights);
        }

     
    }
}
