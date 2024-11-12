using AirportSystem.Data;
using AirportSystem.Entity;
using AirportSystem.Forms;
using AirportSystem.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AirportSystem.Exceptions;
using AirportSystem.Forms.Validate;
using Microsoft.AspNetCore.Identity;
namespace AirportSystem.Services
{
    public class TicketService : ITicketService
    {
        private readonly ApplicationDbContext _context;
        private readonly IFlightService _flightService;
        private readonly ValidateForm _validateForm;
        private readonly UserManager<ApplicationUser> _userManager;
        public TicketService(ApplicationDbContext context,
            IFlightService flightService,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
            _flightService = flightService;
            _validateForm = new ValidateForm(userManager, context, flightService);
        }
        
       

        public async Task<Ticket> CreateTicketWithDetailsAsync(TicketFormModel ticketModel, Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null || !await IsUserInRoleAsync(user, "TicketCashier") && !await IsUserInRoleAsync(user, "Admin"))
            {
                throw new UnauthorizedAccessException("You do not have permission to create tickets.");
            }

            await _validateForm.ValidateTicketFormModel(ticketModel);

            if (!ticketModel.CanRegisterExtraPayload)
            {
                if (ticketModel.TicketClass == TicketClass.Economy && ticketModel.PassengerPayload > 30)
                {
                    throw new AirportSystemException("You cannot register that ticket because you have more than the maximum of the economy payload.");
                }

                if (ticketModel.TicketClass == TicketClass.Business && ticketModel.PassengerPayload > 45)
                {
                    throw new AirportSystemException("You cannot register that ticket because you have more than the maximum of the business payload.");
                }
            }
            else
            {
                var flight = await _context.Flights.Include(f => f.Plane).FirstOrDefaultAsync(f => f.Id == ticketModel.FlightId);
                if (flight != null)
                {
                    var totalPayload = await _context.Tickets.Where(t => t.FlightId == ticketModel.FlightId).SumAsync(t => t.passenger_payload);
                    if (totalPayload + ticketModel.PassengerPayload > flight.Plane.Plane_Payload)
                    {
                        throw new AirportSystemException("Cannot register extra payload because it exceeds the total plane payload.");
                    }
                }
            }

            var ticket = new Ticket
            {
                Id = Guid.NewGuid(),
                TicketClass = ticketModel.TicketClass.ToString(),
                seatNumber = ticketModel.SeatNumber,
                passenger_payload = ticketModel.PassengerPayload,
                GateId = ticketModel.GateId,
                PassengerId = ticketModel.PassengerId,
                TicketCashierId = userId, // Set the current user's ID
                FlightId = ticketModel.FlightId,
                CanRegisterExtraPayload = ticketModel.CanRegisterExtraPayload
            };

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }


        /*public async Task<Ticket> RegisterExtraPayloadAsync(TicketFormModel ticketModel)
        {
            await _validateForm.ValidateTicketFormModel(ticketModel);

            // Validate the total payload for the flight
            var flight = await _context.Flights.Include(f => f.Plane).FirstOrDefaultAsync(f => f.Id == ticketModel.FlightId);
            if (flight != null)
            {
                var totalPayload = await _context.Tickets.Where(t => t.FlightId == ticketModel.FlightId).SumAsync(t => t.passenger_payload);
                if (totalPayload + ticketModel.PassengerPayload > flight.Plane.Plane_Payload)
                {
                    throw new AirportSystemException("Cannot register extra payload because it exceeds the total plane payload.");
                }
            }
            else if(flight == null)
            { 

                throw new AirportSystemException("Flight not found.");   
            }
            // Create the ticket
            var ticket = new Ticket
            {
                Id = Guid.NewGuid(),
                TicketClass = ticketModel.TicketClass.ToString(),
                seatNumber = ticketModel.SeatNumber,
                passenger_payload = ticketModel.PassengerPayload,
                GateId = ticketModel.GateId,
                PassengerId = ticketModel.PassengerId,
                TicketCashierId = ticketModel.TicketCashierId,
                FlightId = ticketModel.FlightId,
                //CanUpdate = true
            };

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }*/

        public async Task<Ticket> GetTicketByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new AirportSystemException("Invalid ticket ID.");
            }
            return await _context.Tickets
                .Include(t => t.Gate)
                .Include(t => t.Passenger)
                .Include(t => t.TicketCashier)
                .Include(t => t.Flight)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
        {
            return await _context.Tickets
                .Include(t => t.Gate)
                .Include(t => t.Passenger)
                .Include(t => t.TicketCashier)
                .Include(t => t.Flight)
                .ToListAsync();
        }

        public async Task<Ticket> UpdateTicketAsync(Guid id, TicketUpdateFormModel ticketModel, Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null || !await IsUserInRoleAsync(user, "TicketCashier") && !await IsUserInRoleAsync(user, "Admin"))
            {
                throw new UnauthorizedAccessException("You do not have permission to update tickets.");
            }
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                throw new Exception("Ticket not found");
            }

            if (!ticket.CanUpdate)
            {
                throw new AirportSystemException("This ticket can no longer be updated.");
            }

            if (!ticketModel.CanRegisterExtraPayload)
            {
                if (ticketModel.TicketClass != null)
                {
                    ticket.TicketClass = ticketModel.TicketClass;
                    if (ticketModel.PassengerPayload > 0)
                    {
                        if (ticketModel.TicketClass == TicketClass.Economy.ToString() && ticketModel.PassengerPayload > 30)
                        {
                            throw new AirportSystemException("You cannot register that ticket because you have more than the maximum of the economy payload.");
                        }

                        if (ticketModel.TicketClass == TicketClass.Business.ToString() && ticketModel.PassengerPayload > 45)
                        {
                            throw new AirportSystemException("You cannot register that ticket because you have more than the maximum of the business payload.");
                        }

                        ticket.passenger_payload = ticketModel.PassengerPayload;
                    }
                }
            }
            else
            {
                // Validate the total payload for the flight
                var flight = await _context.Flights.Include(f => f.Plane).FirstOrDefaultAsync(f => f.Id == ticketModel.FlightId);
                if (flight != null)
                {
                    var totalPayload = await _context.Tickets.Where(t => t.FlightId == ticketModel.FlightId).SumAsync(t => t.passenger_payload);
                    if (totalPayload + ticketModel.PassengerPayload > flight.Plane.Plane_Payload)
                    {
                        throw new AirportSystemException("Cannot register extra payload because it exceeds the total plane payload.");
                    }
                }
            }


            if (ticketModel.SeatNumber != null)
            {
                ticket.seatNumber = ticketModel.SeatNumber;
            }

            if (ticketModel.GateId != Guid.Empty)
            {
                ticket.GateId = ticketModel.GateId;
            }

            if (ticketModel.FlightId != Guid.Empty)
            {
                ticket.FlightId = ticketModel.FlightId;
            }

            ticket.CanUpdate = false;
            ticket.CanRegisterExtraPayload = ticketModel.CanRegisterExtraPayload;
            await _context.SaveChangesAsync();
            return ticket;
        }

        public async Task<ApplicationUser> GetUserByIdAsync(Guid userId)
        {
            return await _userManager.FindByIdAsync(userId.ToString());
        }
        public async Task<bool> IsUserInRoleAsync(ApplicationUser user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<bool> DeleteTicketAsync(Guid id, Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null || !await IsUserInRoleAsync(user, "TicketCashier") && !await IsUserInRoleAsync(user, "Admin"))
            {
                throw new UnauthorizedAccessException("You do not have permission to delete tickets.");
            }
            if (id == Guid.Empty)
            {
                throw new AirportSystemException("Invalid ticket ID.");
            }
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                throw new AirportSystemException("Ticket not found");
            }

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<IEnumerable<Ticket>> GetTicketsByClassAsync(string ticketClass)
        {
            if (string.IsNullOrEmpty(ticketClass))
            {
                throw new AirportSystemException("Ticket class cannot be empty.");
            }
            return await _context.Tickets
                .Where(t => t.TicketClass.ToLower() == ticketClass.ToLower())
                .ToListAsync();
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByFlightIdAsync(Guid flightId)
        {
            if (flightId == Guid.Empty)
            {
                throw new AirportSystemException("Invalid flight ID.");
            }
            return await _context.Tickets
                .Where(t => t.FlightId == flightId)
                .ToListAsync();
        }


        public async Task<IEnumerable<Ticket>> GetTicketsByDurationAsync(TimeSpan duration)
        {
            if (duration <= TimeSpan.Zero)
            {
                throw new AirportSystemException("Duration must be greater than zero.");
            }

            return await _context.Tickets
                .Include(t => t.Flight)
                .Where(t => t.Flight.FlightDuration >= duration)
                .ToListAsync();
        }
    }
}
