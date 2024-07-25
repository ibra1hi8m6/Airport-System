using AirportSystem.Data;
using AirportSystem.Entity;
using AirportSystem.Forms;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportSystem.Services
{
    public class GateService : IGateService
    {
        private readonly ApplicationDbContext _context;

        public GateService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Gate> CreateGateAsync(GateServiceFormModel gateForm)
        {
            var gate = new Gate
            {
                Id = Guid.NewGuid(),
                Name = gateForm.Name
            };

            _context.Gates.Add(gate);
            await _context.SaveChangesAsync();
            return gate;
        }

        public async Task<Gate> GetGateByIdAsync(Guid id)
        {
            return await _context.Gates.FindAsync(id);
        }

        public async Task<IEnumerable<Gate>> GetAllGatesAsync()
        {
            return await _context.Gates.ToListAsync();
        }

        public async Task<Gate> UpdateGateAsync(Guid id, GateServiceFormModel gateForm)
        {
            var gate = await _context.Gates.FindAsync(id);
            if (gate == null)
            {
                return null;
            }

            gate.Name = gateForm.Name;

            await _context.SaveChangesAsync();
            return gate;
        }

        public async Task<bool> DeleteGateAsync(Guid id)
        {
            var gate = await _context.Gates.FindAsync(id);
            if (gate == null)
            {
                return false;
            }

            _context.Gates.Remove(gate);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
