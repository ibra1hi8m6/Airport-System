using AirportSystem.Data;
using AirportSystem.Entity;
using AirportSystem.Exceptions;
using AirportSystem.Forms;
using AirportSystem.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirportSystem.Services
{
    public class FlightService : IFlightService
    {
        private readonly ApplicationDbContext _context;

        public FlightService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Ticket>> GetTicketsByFlightIdAsync(Guid flightId)
        {
            return await _context.Tickets.Where(t => t.FlightId == flightId).ToListAsync();
        }
        public async Task<Flight> CreateFlightAsync(FlightServiceFormModel flightModel)
        {
            await ValidateFlightAsync(flightModel);

            var flight = new Flight
            {
                Id = Guid.NewGuid(),
                DepartureTime = flightModel.DepartureTime,
                ArrivalTime = flightModel.ArrivalTime,
                Takeoff_Location = flightModel.TakeoffLocation,
                Destination = flightModel.Destination,
                PlaneId = flightModel.PlaneId,
                DoctorId = flightModel.DoctorId
            };

            flight.CalculateFlightDuration();

            _context.Flights.Add(flight);
            await _context.SaveChangesAsync();

            var pilotFlight = new PilotFlight
            {
                Id = Guid.NewGuid(),
                PilotUserId = flightModel.PilotId,
                FlightId = flight.Id
            };

            _context.PilotFlights.Add(pilotFlight);
            await _context.SaveChangesAsync();

            return flight;
        }
        public async Task<Flight> GetFlightByIdAsync(Guid id)
        {
            return await _context.Flights.FindAsync(id);
        }

        public async Task<IEnumerable<Flight>> GetAllFlightsAsync()
        {
            return await _context.Flights.ToListAsync();
        }

        public async Task UpdateFlightAsync(Guid id, FlightServiceFormModel flightModel)
        {
            var flight = await GetFlightByIdAsync(id);

            if (flight == null)
            {
                throw new Exception("Flight not found");
            }

            await ValidateFlightAsync(flightModel, id);

            flight.DepartureTime = flightModel.DepartureTime;
            flight.ArrivalTime = flightModel.ArrivalTime;
            flight.Takeoff_Location = flightModel.TakeoffLocation;
            flight.Destination = flightModel.Destination;
            flight.PlaneId = flightModel.PlaneId;

            flight.CalculateFlightDuration();

            _context.Flights.Update(flight);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Flight>> GetFlightsWithDoctorsAsync()
        {
            return await _context.Flights.Where(f => f.DoctorId.HasValue).ToListAsync();
        }

        public async Task<IEnumerable<Flight>> GetFlightsWithPassengersAgeGreaterThanAsync(int age)
        {
            return await _context.Flights
                .Where(f => _context.Tickets
                    .Any(t => t.FlightId == f.Id && _context.Users.OfType<PassengerUser>().Any(p => p.Id == t.PassengerId && p.GetAge() >= age)))
                .ToListAsync();
        }
        public async Task<IEnumerable<Flight>> GetFlightsByDurationAsync(TimeSpan duration, bool greaterThan)
        {
            return await _context.Flights
                .Where(f => greaterThan ? f.FlightDuration > duration : f.FlightDuration < duration)
                .ToListAsync();
        }
        public async Task DeleteFlightAsync(Guid id)
        {
            var flight = await GetFlightByIdAsync(id);

            if (flight == null)
            {
                throw new AirportSystemException("Flight not found");
            }

            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();
        }

        private async Task ValidateFlightAsync(FlightServiceFormModel flightModel, Guid? flightId = null)
        {
            // Check if the plane exists
            var plane = await _context.Planes.FindAsync(flightModel.PlaneId);
            if (plane == null)
            {
                throw new AirportSystemException("Plane not found");
            }

            // Check if the pilot exists and is a pilot user
            var pilot = await _context.Users.FindAsync(flightModel.PilotId);
            var pilotUser = pilot as PilotUser;
            if (pilotUser == null)
            {
                throw new AirportSystemException("Pilot not found");
            }

            // Check if the doctor exists and is a doctor user
            if (flightModel.DoctorId.HasValue)
            {
                var doctor = await _context.Users.FindAsync(flightModel.DoctorId.Value);
                var doctorUser = doctor as DoctorUser;
                if (doctorUser == null)
                {
                    throw new AirportSystemException("Doctor not found");
                }
            }

            // Validation: Flight must be scheduled at least one hour from now
            if (flightModel.DepartureTime <= DateTime.UtcNow.AddHours(1))
            {
                throw new AirportSystemException("Cannot register a flight in the past or less than one hour from now.");
            }

            // Validation: ArrivalTime must be after DepartureTime
            if (flightModel.ArrivalTime <= flightModel.DepartureTime)
            {
                throw new AirportSystemException("Arrival time must be after departure time.");
            }

            var flightDuration = flightModel.ArrivalTime - flightModel.DepartureTime;

            // Rule 1: No same plane on the same route with same departure and arrival times unless duration between them is doubled
            var overlappingFlights = await _context.Flights
                .Where(f => f.PlaneId == flightModel.PlaneId &&
                            f.Takeoff_Location == flightModel.TakeoffLocation &&
                            f.Destination == flightModel.Destination &&
                            (f.DepartureTime == flightModel.DepartureTime || f.ArrivalTime == flightModel.ArrivalTime) &&
                            (!flightId.HasValue || f.Id != flightId.Value)) // Exclude current flight in case of update
                .ToListAsync();

            foreach (var overlappingFlight in overlappingFlights)
            {
                if (Math.Abs((overlappingFlight.DepartureTime - flightModel.DepartureTime).TotalMinutes) < (2 * flightDuration.TotalMinutes) ||
                    Math.Abs((overlappingFlight.ArrivalTime - flightModel.ArrivalTime).TotalMinutes) < (2 * flightDuration.TotalMinutes))
                {
                    throw new AirportSystemException("Cannot register a flight on the same plane with the same departure and arrival times without a sufficient time gap.");
                }
            }

            // Rule 2: No two flights with the same route and same times unless the route is clear for the duration of the first flight
            var conflictingFlights = await _context.Flights
                .Where(f => f.Takeoff_Location == flightModel.TakeoffLocation &&
                            f.Destination == flightModel.Destination &&
                            (f.DepartureTime == flightModel.DepartureTime || f.ArrivalTime == flightModel.ArrivalTime) &&
                            (!flightId.HasValue || f.Id != flightId.Value)) // Exclude current flight in case of update
                .ToListAsync();

            foreach (var conflictingFlight in conflictingFlights)
            {
                if (Math.Abs((conflictingFlight.DepartureTime - flightModel.DepartureTime).TotalMinutes) < flightDuration.TotalMinutes ||
                    Math.Abs((conflictingFlight.ArrivalTime - flightModel.ArrivalTime).TotalMinutes) < flightDuration.TotalMinutes)
                {
                    throw new AirportSystemException("Cannot register two flights on the same route with overlapping times.");
                }
            }

            // Check pilot's eligibility based on age and total flight hours
            int pilotAge = pilotUser.GetAge();
            if (pilotAge < 35 && pilotUser.TotalHours >= 150 && flightDuration.TotalHours > 5)
            {
                throw new AirportSystemException("Pilot younger than 35 years cannot register for flights longer than 5 hours.");
            }
        }
    }
}
