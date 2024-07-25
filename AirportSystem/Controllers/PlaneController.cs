using AirportSystem.Entity;
using AirportSystem.Forms;
using AirportSystem.Services;
using AirportSystem.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaneController : ControllerBase
    {
        private readonly IPlaneService _planeService;

        public PlaneController(IPlaneService planeService)
        {
            _planeService = planeService;
        }

        [HttpPost]
        public async Task<ActionResult<Plane>> CreatePlane([FromBody] PlaneServiceFormModel planeForm)
        {
            var plane = await _planeService.CreatePlaneAsync(planeForm);
            return CreatedAtAction(nameof(GetPlaneById), new { id = plane.Id }, plane);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Plane>> GetPlaneById(Guid id)
        {
            var plane = await _planeService.GetPlaneByIdAsync(id);
            if (plane == null)
            {
                return NotFound();
            }
            return Ok(plane);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plane>>> GetAllPlanes()
        {
            var planes = await _planeService.GetAllPlanesAsync();
            return Ok(planes);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Plane>> UpdatePlane(Guid id, [FromBody] PlaneServiceFormModel planeForm)
        {
            var plane = await _planeService.UpdatePlaneAsync(id, planeForm);
            if (plane == null)
            {
                return NotFound();
            }
            return Ok(plane);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePlane(Guid id)
        {
            var result = await _planeService.DeletePlaneAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
