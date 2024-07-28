using AirportSystem.Entity;
using AirportSystem.Forms;
using AirportSystem.Services;
using AirportSystem.Services.IServices;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Plane>> CreatePlane([FromBody] PlaneServiceFormModel planeForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var (plane, message) = await _planeService.CreatePlaneAsync(planeForm);
                return Ok(new { plane, message });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Plane>> UpdatePlane(Guid id, [FromBody] PlaneServiceFormModel planeForm)
        {
            try
            {
                var plane = await _planeService.UpdatePlaneAsync(id, planeForm);
                if (plane == null)
                {
                    return NotFound();
                }
                return Ok(plane);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("update-even-payload/{id}")]
        public async Task<ActionResult<Plane>> UpdatePlaneWithEvenPayload(Guid id, [FromBody] PlaneServiceFormModel planeForm)
        {
            try
            {
                var plane = await _planeService.UpdatePlaneWithEvenPayloadAsync(id, planeForm);
                if (plane == null)
                {
                    return NotFound();
                }
                return Ok(plane);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
