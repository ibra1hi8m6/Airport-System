using AirportSystem.Entity;
using AirportSystem.Exceptions;
using AirportSystem.Forms;
using AirportSystem.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateFlight([FromBody] FlightServiceFormModel flightModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var flight = await _flightService.CreateFlightAsync(flightModel);
                return Ok(flight);
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

        [HttpGet("{id}")]
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

        [HttpGet("with-doctors")]
        public async Task<IActionResult> GetFlightsWithDoctors()
        {
            var flights = await _flightService.GetFlightsWithDoctorsAsync();
            return Ok(flights);
        }

        [HttpGet("passenger-age-greater-than/{age}")]
        public async Task<IActionResult> GetFlightsWithPassengersAgeGreaterThan(int age)
        {
            var flights = await _flightService.GetFlightsWithPassengersAgeGreaterThanAsync(age);
            return Ok(flights);
        }

        [HttpGet("duration-greater-than/{hours}")]
        public async Task<IActionResult> GetFlightsWithDurationGreaterThan(int hours)
        {
            var duration = TimeSpan.FromHours(hours);
            var flights = await _flightService.GetFlightsByDurationAsync(duration, true);
            return Ok(flights);
        }

        [HttpGet("duration-less-than/{hours}")]
        public async Task<IActionResult> GetFlightsWithDurationLessThan(int hours)
        {
            var duration = TimeSpan.FromHours(hours);
            var flights = await _flightService.GetFlightsByDurationAsync(duration, false);
            return Ok(flights);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
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
    }
}
