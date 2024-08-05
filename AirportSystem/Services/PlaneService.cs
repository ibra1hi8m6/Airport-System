using AirportSystem.Data;
using AirportSystem.Entity;
using AirportSystem.Exceptions;
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

        public async Task<(Plane plane, string message)> CreatePlaneAsync(PlaneServiceFormModel planeForm, string userRole)
        {
            if (userRole != "Admin")
            {
                throw new UnauthorizedAccessException("Only admins can create planes.");
            }
            // Define the maximum payloads for each seat type
            const int maxEconomyPayload = 30;
            const int maxBusinessPayload = 45;

            // Calculate the total payload based on the number of seats and their maximum payloads
            int economyPayload = planeForm.seats_Economy * maxEconomyPayload;
            int businessPayload = planeForm.seats_Budiness * maxBusinessPayload;
            int totalPayload = economyPayload + businessPayload;

            // Ensure the plane's payload is valid
            if (totalPayload >= planeForm.Plane_Payload)
            {
                throw new AirportSystemException($"Total payload of seats ({totalPayload}) exceeds the plane's payload ({planeForm.Plane_Payload}).");
            }

           

            var plane = new Plane
            {
                Id = Guid.NewGuid(),
                Plane_model = planeForm.Plane_model,
                Plane_Payload = planeForm.Plane_Payload,
                seats_Economy = planeForm.seats_Economy,
                seats_Business = planeForm.seats_Budiness
            };

            _context.Planes.Add(plane);
            await _context.SaveChangesAsync();


            string message = null;
            // Ensure the plane's payload is evenly divisible by the sum of the payloads for economy and business seats
            if (planeForm.Plane_Payload % (maxEconomyPayload + maxBusinessPayload) != 0)
            {
                message = $"Plane created, but payload must be evenly divisible by the sum of economy and business payloads ({maxEconomyPayload + maxBusinessPayload}). You can update it via the API update.";
            }

            return (plane, message);
        }

        public async Task<Plane> GetPlaneByIdAsync(Guid id)
        {
            return await _context.Planes.FindAsync(id);
        }

        public async Task<IEnumerable<Plane>> GetAllPlanesAsync()
        {
            return await _context.Planes.ToListAsync();
        }

        public async Task<Plane> UpdatePlaneAsync(Guid id, PlaneServiceFormModel planeForm, string userRole)
        {
            if (userRole != "Admin")
            {
                throw new UnauthorizedAccessException("Only admins can update planes.");
            }
            var plane = await _context.Planes.FindAsync(id);
            if (plane == null)
            {
                return null;
            }

            // Define the maximum payloads for each seat type
            const int maxEconomyPayload = 30;
            const int maxBusinessPayload = 45;

            // Calculate the total payload based on the number of seats and their maximum payloads
            int economyPayload = planeForm.seats_Economy * maxEconomyPayload;
            int businessPayload = planeForm.seats_Budiness * maxBusinessPayload;
            int totalPayload = economyPayload + businessPayload;

            // Ensure the plane's payload is valid
            if (totalPayload > planeForm.Plane_Payload)
            {
                throw new AirportSystemException($"Total payload of seats ({totalPayload}) exceeds the plane's payload ({planeForm.Plane_Payload}).");
            }

            plane.Plane_model = planeForm.Plane_model;
            plane.Plane_Payload = planeForm.Plane_Payload;
            plane.seats_Economy = planeForm.seats_Economy;
            plane.seats_Business = planeForm.seats_Budiness;

            await _context.SaveChangesAsync();

            return plane;
        }

        public async Task<Plane> UpdatePlaneWithEvenPayloadAsync(Guid id, PlaneServiceFormModel planeForm, string userRole)
        {
            if (userRole != "Admin")
            {
                throw new UnauthorizedAccessException("Only admins can update planes.");
            }

            var plane = await _context.Planes.FindAsync(id);
            if (plane == null)
            {
                return null;
            }

            // Define the maximum payloads for each seat type
            const int maxEconomyPayload = 30;
            const int maxBusinessPayload = 45;

            // Calculate the total payload based on the number of seats and their maximum payloads
            int totalPayload = (maxEconomyPayload + maxBusinessPayload);

            // Ensure the plane's payload is evenly divisible by the sum of the payloads for economy and business seats
            if (planeForm.Plane_Payload % totalPayload != 0)
            {
                throw new AirportSystemException($"Plane payload must be evenly divisible by the sum of economy and business payloads ({totalPayload}).");
            }

            plane.Plane_model = planeForm.Plane_model;
            plane.Plane_Payload = planeForm.Plane_Payload;
            plane.seats_Economy = planeForm.seats_Economy;
            plane.seats_Business = planeForm.seats_Budiness;

            await _context.SaveChangesAsync();

            return plane;
        }

        public async Task<bool> DeletePlaneAsync(Guid id, string userRole)
        {
            if (userRole != "Admin")
            {
                throw new UnauthorizedAccessException("Only admins can delete planes.");
            }
            var plane = await _context.Planes.FindAsync(id);
            if (plane == null)
            {
                return false;
            }

            _context.Planes.Remove(plane);
            await _context.SaveChangesAsync();
            return true;
        }

       

        public async Task<IEnumerable<Plane>> GetPlanesByPayloadAsync(int payload)
        {
            return await _context.Planes
                .Where(plane => plane.Plane_Payload == payload)
                .ToListAsync();
        }

        public async Task<IEnumerable<Plane>> GetPlanesBySeatsAsync(int seats)
        {
            return await _context.Planes
                .Where(plane => plane.seats_Economy + plane.seats_Business == seats)
                .ToListAsync();
        }
    }
}
