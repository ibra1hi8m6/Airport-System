using AirportSystem.Entity;
using AirportSystem.Forms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportSystem.Services
{
    public interface IGateService
    {
        Task<Gate> CreateGateAsync(GateServiceFormModel gateForm, string userRole);
        Task<Gate> GetGateByIdAsync(Guid id);
        Task<IEnumerable<Gate>> GetAllGatesAsync();
        Task<(IEnumerable<Gate> gates, int totalPages)> GetGatesWithPaginationAsync(int page, int pageSize);
        Task<Gate> UpdateGateAsync(Guid id, GateServiceFormModel gateForm, string userRole);
        Task<bool> DeleteGateAsync(Guid id, string userRole);
    }
}
