using AirportSystem.Entity;
using AirportSystem.Forms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportSystem.Services.IServices
{
    public interface IFlightService
    {
        Task<IEnumerable<Flight>> GetFlightsWithDoctorsAsync();
        Task<IEnumerable<Flight>> GetFlightsWithPassengersAgeGreaterThanAsync(int age);
        Task<IEnumerable<Flight>> GetFlightsByDurationAsync(TimeSpan duration, bool greaterThan);
        Task<IEnumerable<Ticket>> GetTicketsByFlightIdAsync(Guid flightId);
        Task<Flight> CreateFlightAsync(FlightServiceFormModel flightModel);
        Task<Flight> GetFlightByIdAsync(Guid id);
        Task<IEnumerable<Flight>> GetAllFlightsAsync();
        Task UpdateFlightAsync(Guid id, FlightServiceFormModel flightModel);
        Task DeleteFlightAsync(Guid id);
    }
}
