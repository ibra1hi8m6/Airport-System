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
namespace AirportSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly ApplicationDbContext _context;

        public TicketController(ITicketService ticketService, ApplicationDbContext context)
        {
            _ticketService = ticketService;
            _context = context;
        }
        [Authorize(Roles = "TicketCashier")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateTicket([FromBody] TicketFormModel ticketModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var ticket = await _ticketService.CreateTicketAsync(ticketModel);

                // Fetch additional details
                var flight = await _context.Flights.Include(f => f.Plane).FirstOrDefaultAsync(f => f.Id == ticket.FlightId);
                var passenger = await _context.Users.OfType<PassengerUser>().FirstOrDefaultAsync(p => p.Id == ticket.PassengerId);

                var ticketResponse = new TicketResponseModel
                {
                    TicketId = ticket.Id,
                    TicketClass = ticket.TicketClass,
                    PassengerPayload = ticket.passenger_payload,
                    SeatNumber = ticket.seatNumber,
                    GateId = ticket.GateId,
                    PassengerId = ticket.PassengerId,
                    PassengerName = passenger?.UserName,
                    TicketCashierId = ticket.TicketCashierId,
                    FlightId = ticket.FlightId,
                    FlightDetails = $"Flight from {flight?.Takeoff_Location} to {flight?.Destination} at {flight?.DepartureTime}",
                    PlaneId = flight?.Plane.Id ?? Guid.Empty
                };

                return Ok(ticketResponse);
            }
            catch (AirportSystemException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
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

        [HttpPut("{id}")]
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
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("register-extra-payload")]
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
        }

        [HttpDelete("{id}")]
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
    }
}
