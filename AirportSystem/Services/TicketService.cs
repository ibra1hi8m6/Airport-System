using AirportSystem.Data;
using AirportSystem.Entity;
using AirportSystem.Forms;
using AirportSystem.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AirportSystem.Exceptions;
namespace AirportSystem.Services
{
    public class TicketService : ITicketService
    {
        private readonly ApplicationDbContext _context;
        private readonly IFlightService _flightService;
        public TicketService(ApplicationDbContext context, IFlightService flightService)
        {
            _context = context;
            _flightService = flightService;
        }
        private async Task ValidateTicketFormModel(TicketFormModel ticketModel)
        {
      

            // Validate seat availability
            var flight = await _context.Flights.Include(f => f.Plane).FirstOrDefaultAsync(f => f.Id == ticketModel.FlightId);
            if (flight == null)
            {
                throw new AirportSystemException("Flight not found.");
            }

            var tickets = await _flightService.GetTicketsByFlightIdAsync(ticketModel.FlightId);
            var economyTickets = tickets.Count(t => t.TicketClass == TicketClass.Economy.ToString());
            var businessTickets = tickets.Count(t => t.TicketClass == TicketClass.Business.ToString());

            if (ticketModel.TicketClass == TicketClass.Economy && economyTickets >= flight.Plane.seats_Economy)
            {
                throw new AirportSystemException("The number of seats in economy is full.");
            }

            if (ticketModel.TicketClass == TicketClass.Business && businessTickets >= flight.Plane.seats_Business)
            {
                throw new AirportSystemException("The number of seats in business is full.");
            }

            // Check passenger age and doctor presence
            var passenger = await _context.Users.OfType<PassengerUser>().FirstOrDefaultAsync(p => p.Id == ticketModel.PassengerId);
            if (passenger == null)
            {
                throw new AirportSystemException("Passenger not found.");
            }

            var hasDoctor = flight.DoctorId != null;
            if (passenger.GetAge() > 50 && !hasDoctor)
            {
                throw new AirportSystemException("Cannot register the ticket because the passenger is older than 50 years and the flight does not have a doctor.");
            }

            // Check passenger age and flight duration
            if (passenger.GetAge() < 15 && flight.FlightDuration > TimeSpan.FromHours(5))
            {
                throw new AirportSystemException("Cannot register the ticket because the passenger is younger than 15 years and the flight duration exceeds 5 hours.");
            }

            // Check if the same passenger is already registered for the same flight
            var existingTicket = await _context.Tickets.FirstOrDefaultAsync(t => t.FlightId == ticketModel.FlightId && t.PassengerId == ticketModel.PassengerId);
            if (existingTicket != null)
            {
                throw new AirportSystemException("The passenger is already registered for this flight.");
            }

           
        }

        public async Task<Ticket> CreateTicketAsync(TicketFormModel ticketModel)
        {
            await ValidateTicketFormModel(ticketModel);

            // Validate ticket class and payload
            if (ticketModel.TicketClass == TicketClass.Economy && ticketModel.PassengerPayload > 30)
            {
                throw new AirportSystemException("You cannot register that ticket because you have more than the maximum of the economy payload.");
            }

            if (ticketModel.TicketClass == TicketClass.Business && ticketModel.PassengerPayload > 45)
            {
                throw new AirportSystemException("You cannot register that ticket because you have more than the maximum of the business payload.");
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
        }

        public async Task<Ticket> RegisterExtraPayloadAsync(TicketFormModel ticketModel)
        {
            await ValidateTicketFormModel(ticketModel);

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
        }

        public async Task<Ticket> GetTicketByIdAsync(Guid id)
        {
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

        public async Task<Ticket> UpdateTicketAsync(Guid id, TicketUpdateFormModel ticketModel)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                throw new Exception("Ticket not found");
            }

            if (!ticket.CanUpdate)
            {
                throw new AirportSystemException("This ticket can no longer be updated.");
            }

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

            await _context.SaveChangesAsync();
            return ticket;
        }

       


        public async Task<bool> DeleteTicketAsync(Guid id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                throw new AirportSystemException("Ticket not found");
            }

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
