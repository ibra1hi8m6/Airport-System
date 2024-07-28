using AirportSystem.Entity;
using AirportSystem.Forms;
using AirportSystem.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
namespace AirportSystem.Services.IServices
{
    public interface ITicketService
    {
        Task<Ticket> CreateTicketAsync(TicketFormModel ticketModel);
        Task<Ticket> GetTicketByIdAsync(Guid id);
        Task<IEnumerable<Ticket>> GetAllTicketsAsync();
        Task<Ticket> UpdateTicketAsync(Guid id, TicketUpdateFormModel ticketModel);
        Task<Ticket> RegisterExtraPayloadAsync(TicketFormModel ticketModel);
        Task<bool> DeleteTicketAsync(Guid id);
    }
}
