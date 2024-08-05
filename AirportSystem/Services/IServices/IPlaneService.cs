using AirportSystem.Forms;
using AirportSystem.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportSystem.Services.IServices
{
    public interface IPlaneService
    {
        Task<(Plane plane, string message)> CreatePlaneAsync(PlaneServiceFormModel planeForm, string userRole);
        Task<Plane> GetPlaneByIdAsync(Guid id);
        Task<IEnumerable<Plane>> GetAllPlanesAsync();
        Task<Plane> UpdatePlaneAsync(Guid id, PlaneServiceFormModel planeForm, string userRole);
        Task<Plane> UpdatePlaneWithEvenPayloadAsync(Guid id, PlaneServiceFormModel planeForm, string userRole);
        Task<bool> DeletePlaneAsync(Guid id, string userRole);
        
        Task<IEnumerable<Plane>> GetPlanesByPayloadAsync(int payload);
        Task<IEnumerable<Plane>> GetPlanesBySeatsAsync(int seats);
    }
}
