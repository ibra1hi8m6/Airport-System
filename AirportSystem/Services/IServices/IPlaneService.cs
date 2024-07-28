using AirportSystem.Forms;
using AirportSystem.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportSystem.Services.IServices
{
    public interface IPlaneService
    {
        Task<(Plane plane, string message)> CreatePlaneAsync(PlaneServiceFormModel planeForm);
        Task<Plane> GetPlaneByIdAsync(Guid id);
        Task<IEnumerable<Plane>> GetAllPlanesAsync();
        Task<Plane> UpdatePlaneAsync(Guid id, PlaneServiceFormModel planeForm);
        Task<Plane> UpdatePlaneWithEvenPayloadAsync(Guid id, PlaneServiceFormModel planeForm);
        Task<bool> DeletePlaneAsync(Guid id);
    }
}
