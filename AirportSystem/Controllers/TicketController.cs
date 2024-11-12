using AirportSystem.Forms;
using AirportSystem.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AirportSystem.Exceptions;
using AirportSystem.Entity;
using Microsoft.EntityFrameworkCore;
using AirportSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using AirportSystem.Forms.Validate;
using AirportSystem.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
namespace AirportSystem.Controllers
{
    [Route("api/Tickets")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly ApplicationDbContext _context;
        private readonly ValidateForm _validateForm;

        public TicketController(UserManager<ApplicationUser> userManager,
            ITicketService ticketService,
            IFlightService flightService,
            ApplicationDbContext context)
        {
            _ticketService = ticketService;
            _context = context;
            _validateForm = new ValidateForm(userManager, context, flightService);
        }
        [Authorize(Roles = "TicketCashier,Admin")]
        [HttpPost("CreateTicket")]
        public async Task<IActionResult> CreateTicket([FromBody] TicketFormModel ticketModel)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                var ticketResponse = await _ticketService.CreateTicketWithDetailsAsync(ticketModel, userId);
                return Ok(ticketResponse);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (AirportSystemException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
           
        }

        [HttpGet("GetTicketById/{id}")]
        public async Task<IActionResult> GetTicketById(Guid id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return Ok(ticket);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTickets()
        {
            var tickets = await _ticketService.GetAllTicketsAsync();
            return Ok(tickets);
        }
        [HttpPut("UpdateTicket/{id}")]
        public async Task<IActionResult> UpdateTicket(Guid id, [FromBody] TicketUpdateFormModel ticketModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
        

                var updatedTicket = await _ticketService.UpdateTicketAsync(id, ticketModel);
                return Ok(updatedTicket);
            }
            catch (AirportSystemException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
          
        }

        /*[HttpPost("RegisterExtraPayload")]
        public async Task<IActionResult> RegisterExtraPayload([FromBody] TicketFormModel ticketModel)
        {
            try
            {
                var ticket = await _ticketService.RegisterExtraPayloadAsync(ticketModel);
                return Ok(ticket);
            }
            catch (AirportSystemException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }*/

        [HttpDelete("DeleteTicket/{id}")]
        public async Task<IActionResult> DeleteTicket(Guid id)
        {
            try
            {
                var result = await _ticketService.DeleteTicketAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("GetEconomySeats")]
        public async Task<IActionResult> GetEconomySeats()
        {
            var tickets = await _ticketService.GetTicketsByClassAsync("Economy");
            return Ok(tickets);
        }

        [HttpGet("GetBusinessSeats")]
        public async Task<IActionResult> GetBusinessSeats()
        {
            var tickets = await _ticketService.GetTicketsByClassAsync("Business");
            return Ok(tickets);
        }

        [HttpGet("GetTicketsByFlight/{flightId}")]
        public async Task<IActionResult> GetTicketsByFlight(Guid flightId)
        {
            var tickets = await _ticketService.GetTicketsByFlightIdAsync(flightId);
            return Ok(tickets);
        }



        [HttpGet("GetTicketsByDuration")]
        public async Task<IActionResult> GetTicketsByDuration([FromQuery] int hours)
        {
            try
            {
                var duration = TimeSpan.FromHours(hours);
                var tickets = await _ticketService.GetTicketsByDurationAsync(duration);
                return Ok(tickets);
            }
            catch (AirportSystemException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
