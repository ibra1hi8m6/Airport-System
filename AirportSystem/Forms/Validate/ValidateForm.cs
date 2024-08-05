using AirportSystem.Data;
using AirportSystem.Entity;
using AirportSystem.Forms;
using AirportSystem.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using AirportSystem.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace AirportSystem.Forms.Validate
{
    public interface IValidateForm
    {
        Task<string> ValidateSignUpForm(SignUpFormModel model);
        Task ValidateTicketFormModel(TicketFormModel ticketModel);
    }

    public class ValidateForm : IValidateForm
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IFlightService _flightService;

        public ValidateForm(UserManager<ApplicationUser> userManager,
            ApplicationDbContext context,
            IFlightService flightService)
        {
            _userManager = userManager;
            _context = context;
            _flightService = flightService;
        }

        public async Task<string> ValidateSignUpForm(SignUpFormModel model)
        {
            // General validations
            if (string.IsNullOrEmpty(model.Username))
                return "Username is required.";
            if (string.IsNullOrEmpty(model.FirstName))
                return "FirstName is required.";
            if (string.IsNullOrEmpty(model.LastName))
                return "LastName is required.";
            if (string.IsNullOrEmpty(model.Email))
                return "Email is required.";
            if (string.IsNullOrEmpty(model.Password))
                return "Password is required.";
            if (string.IsNullOrEmpty(model.PhoneNumber))
                return "PhoneNumber is required.";

            if (model.Password != model.ConfirmPassword)
                return "Passwords do not match.";

            // Check if username or email already exists
            var existingUserByUsername = await _userManager.FindByNameAsync(model.Username);
            var existingUserByEmail = await _userManager.FindByEmailAsync(model.Email);

            if (existingUserByUsername != null || existingUserByEmail != null)
            {
                return "A user with the same username or email already exists.";
            }

            // Role-specific validations
            switch (model.Role)
            {
                case UserRole.Pilot:
                    if (model.TotalHours == null)
                        return "TotalHours is required for Pilot role.";
                    break;

                case UserRole.Passenger:
                    if (model.HouseNumber == null)
                        return "HouseNumber is required for Passenger role.";
                    if (string.IsNullOrEmpty(model.Street))
                        return "Street is required for Passenger role.";
                    if (string.IsNullOrEmpty(model.City))
                        return "City is required for Passenger role.";
                    if (string.IsNullOrEmpty(model.Country))
                        return "Country is required for Passenger role.";
                    break;

                case UserRole.Doctor:
                    if (string.IsNullOrEmpty(model.DoctorCode))
                        return "DoctorCode is required for Doctor role.";
                    break;

                // Handle cases where the role might not be recognized or require special validation
                default:
                    return "Invalid role.";
            }

            return null; // No validation errors
        }

        public async Task ValidateTicketFormModel(TicketFormModel ticketModel)
        {
            // Check for null or zero values
            ValidateTicketFormFields(ticketModel);


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
        public void ValidateTicketFormFields(TicketFormModel ticketModel)
        {
            if (ticketModel == null)
            {
                throw new AirportSystemException("The form cannot be null.");
            }

            if (ticketModel.FlightId == Guid.Empty)
            {
                throw new AirportSystemException("Flight ID cannot be empty.");
            }

            if (ticketModel.PassengerId == Guid.Empty)
            {
                throw new AirportSystemException("Passenger ID cannot be empty.");
            }

            if (ticketModel.SeatNumber == null)
            {
                throw new AirportSystemException("Seat number cannot be null.");
            }

            if (ticketModel.PassengerPayload <= 0)
            {
                throw new AirportSystemException("Passenger payload must be greater than zero.");
            }

            if (ticketModel.TicketCashierId == Guid.Empty)
            {
                throw new AirportSystemException("Ticket Cashier ID cannot be empty.");
            }

            if (ticketModel.GateId == Guid.Empty)
            {
                throw new AirportSystemException("Gate ID cannot be empty.");
            }
        }
    }
}
