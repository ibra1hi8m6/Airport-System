using AirportSystem.Data;
using AirportSystem.Entity;
using AirportSystem.Forms;
using AirportSystem.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportSystem.Services
{
    public class PlaneService : IPlaneService
    {
        private readonly ApplicationDbContext _context;

        public PlaneService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Plane> CreatePlaneAsync(PlaneServiceFormModel planeForm)
        {
            var plane = new Plane
            {
                Id = Guid.NewGuid(),
                Plane_model = planeForm.Plane_model,
                Plane_Payload = planeForm.Plane_Payload,
                number_of_seats = planeForm.Number_of_seats
            };

            _context.Planes.Add(plane);
            await _context.SaveChangesAsync();
            return plane;
        }

        public async Task<Plane> GetPlaneByIdAsync(Guid id)
        {
            return await _context.Planes.FindAsync(id);
        }

        public async Task<IEnumerable<Plane>> GetAllPlanesAsync()
        {
            return await _context.Planes.ToListAsync();
        }

        public async Task<Plane> UpdatePlaneAsync(Guid id, PlaneServiceFormModel planeForm)
        {
            var plane = await _context.Planes.FindAsync(id);
            if (plane == null)
            {
                return null;
            }

            plane.Plane_model = planeForm.Plane_model;
            plane.Plane_Payload = planeForm.Plane_Payload;
            plane.number_of_seats = planeForm.Number_of_seats;

            await _context.SaveChangesAsync();
            return plane;
        }

        public async Task<bool> DeletePlaneAsync(Guid id)
        {
            var plane = await _context.Planes.FindAsync(id);
            if (plane == null)
            {
                return false;
            }

            _context.Planes.Remove(plane);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
