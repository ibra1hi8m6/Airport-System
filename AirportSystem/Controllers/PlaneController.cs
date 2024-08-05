using AirportSystem.Data;
using AirportSystem.Entity;
using AirportSystem.Forms;
using AirportSystem.Services;
using AirportSystem.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AirportSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaneController : ControllerBase
    {
        private readonly IPlaneService _planeService;
        private readonly ApplicationDbContext _context;

        public PlaneController(IPlaneService planeService, ApplicationDbContext context)
        {
            _planeService = planeService;
            _context = context;
        }
        //[Authorize(Roles = "Admin")]
        [HttpPost("CreatePlane")]
        public async Task<ActionResult<Plane>> CreatePlane([FromBody] PlaneServiceFormModel planeForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userRole = User.FindFirstValue(ClaimTypes.Role);
            try
            {
                var (plane, message) = await _planeService.CreatePlaneAsync(planeForm, userRole);
                var planeResponse = new PlaneResponseModel
                {
                    PlaneId = plane.Id,
                    PlaneModel = plane.Plane_model,
                    PlanePayload = plane.Plane_Payload,
                    SeatsEconomy = plane.seats_Economy,
                    SeatsBusiness = plane.seats_Business,
                    Message = message
                };

                return Ok(planeResponse);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetPlaneById/{id}")]
        public async Task<ActionResult<Plane>> GetPlaneById(Guid id)
        {
            var plane = await _planeService.GetPlaneByIdAsync(id);
            if (plane == null)
            {
                return NotFound();
            }
            return Ok(plane);
        }

        [HttpGet("GetAllPlanes")]
        public async Task<ActionResult<IEnumerable<Plane>>> GetAllPlanes()
        {
            var planes = await _planeService.GetAllPlanesAsync();
            return Ok(planes);
        }
        //[Authorize(Roles = "Admin")]
        [HttpPut("UpdatePlane/{id}")]
        public async Task<ActionResult<Plane>> UpdatePlane(Guid id, [FromBody] PlaneServiceFormModel planeForm)
        {
            var userRole = User.FindFirstValue(ClaimTypes.Role); // Get the role from the claims
            try
            {
                var plane = await _planeService.UpdatePlaneAsync(id, planeForm, userRole);
                if (plane == null)
                {
                    return NotFound();
                }
                return Ok(plane);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //[Authorize(Roles = "Admin")]
        [HttpPut("UpdateEvenPayload/{id}")]
        public async Task<ActionResult<Plane>> UpdatePlaneWithEvenPayload(Guid id, [FromBody] PlaneServiceFormModel planeForm)
        {
            var userRole = User.FindFirstValue(ClaimTypes.Role); // Get the role from the claims
            try
            {
                var plane = await _planeService.UpdatePlaneWithEvenPayloadAsync(id, planeForm, userRole);
                if (plane == null)
                {
                    return NotFound();
                }
                return Ok(plane);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeletePlane/{id}")]
        public async Task<ActionResult> DeletePlane(Guid id)
        {
            var userRole = User.FindFirstValue(ClaimTypes.Role); // Get the role from the claims
            try
            {
                var result = await _planeService.DeletePlaneAsync(id, userRole);
                if (!result)
                {
                    return NotFound();
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
                
            }
            return NoContent();
        }

        [HttpGet("GetPlanesByPayload")]
        public async Task<ActionResult<IEnumerable<Plane>>> GetPlanesByPayload([FromQuery] int payload)
        {
            var planes = await _planeService.GetPlanesByPayloadAsync(payload);
            return Ok(planes);
        }

        [HttpGet("GetPlanesBySeats")]
        public async Task<ActionResult<IEnumerable<Plane>>> GetPlanesBySeats([FromQuery] int seats)
        {
            var planes = await _planeService.GetPlanesBySeatsAsync(seats);
            return Ok(planes);
        }
    }
}
