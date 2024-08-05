using AirportSystem.Data;
using AirportSystem.Entity;
using AirportSystem.Exceptions;
using AirportSystem.Forms;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public async Task<Gate> CreateGateAsync(GateServiceFormModel gateForm, string userRole)
        {
            if (userRole != "Admin")
            {
                throw new UnauthorizedAccessException("Only admins can create gates.");
            }
            var existingGate = await _context.Gates.FirstOrDefaultAsync(g => g.Name == gateForm.Name);
            if (existingGate != null)
            {
                throw new Exception("A gate with the same name already exists.");
            }
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
            if (id == Guid.Empty)
            {
                throw new AirportSystemException("Gate ID cannot be empty.");
            }
            return await _context.Gates.FindAsync(id);
        }
        public async Task<(IEnumerable<Gate> gates, int totalPages)> GetGatesWithPaginationAsync(int page, int pageSize)
        {
            var totalGates = await _context.Gates.CountAsync();
            var totalPages = (int)Math.Ceiling(totalGates / (double)pageSize);

            var gates = await _context.Gates
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (gates, totalPages);
        }
        public async Task<IEnumerable<Gate>> GetAllGatesAsync()
        {
            return await _context.Gates.ToListAsync();
        }

        public async Task<Gate> UpdateGateAsync(Guid id, GateServiceFormModel gateForm, string userRole)
        {
            if (userRole != "Admin")
            {
                throw new UnauthorizedAccessException("Only admins can update gates.");
            }
            // Validate ID
            if (id == Guid.Empty)
            {
                throw new AirportSystemException("ID cannot be empty.");
            }

            // Validate form parameters
            if (string.IsNullOrEmpty(gateForm.Name))
            {
                throw new AirportSystemException("Gate name cannot be null or empty.");
            }
            var gate = await _context.Gates.FindAsync(id);
            if (gate == null)
            {
                return null;
            }

            gate.Name = gateForm.Name;

            await _context.SaveChangesAsync();
            return gate;
        }

        public async Task<bool> DeleteGateAsync(Guid id, string userRole)
        {
            if (userRole != "Admin")
            {
                throw new UnauthorizedAccessException("Only admins can delete gates.");
            }
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