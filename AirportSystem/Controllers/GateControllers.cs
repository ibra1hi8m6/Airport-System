using AirportSystem.Entity;
using AirportSystem.Exceptions;
using AirportSystem.Forms;
using AirportSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AirportSystem.Controllers
{
    [ApiController]
    [Route("api/Gates")]
    public class GateController : ControllerBase
    {
        private readonly IGateService _gateService;

        public GateController(IGateService gateService)
        {
            _gateService = gateService;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("CreateGate")]
        public async Task<ActionResult<Gate>> CreateGate([FromBody] GateServiceFormModel gateForm)
        {
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            try
            {
                var gate = await _gateService.CreateGateAsync(gateForm, userRole);
                return CreatedAtAction(nameof(GetGateById), new { id = gate.Id }, gate);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            catch (AirportSystemException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("GetGateById/{id}")]
        public async Task<ActionResult<Gate>> GetGateById(Guid id)
        {
            var gate = await _gateService.GetGateByIdAsync(id);
            if (gate == null)
            {
                return NotFound();
            }
            return Ok(gate);
        }

        [HttpGet("GetAllGates")]
        public async Task<ActionResult<IEnumerable<Gate>>> GetAllGates()
        {
            var gates = await _gateService.GetAllGatesAsync();
            return Ok(gates);
        }
        [HttpGet("page/{page}")]
        public async Task<ActionResult<IEnumerable<Gate>>> GetGatesWithPagination(int page, int pageSize = 5)
        {
            var (gates, totalPages) = await _gateService.GetGatesWithPaginationAsync(page, pageSize);
            if (page > totalPages || page < 1)
            {
                return BadRequest("Invalid page number.");
            }
            return Ok(new { gates, totalPages });
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateGate/{id}")]
        public async Task<ActionResult<Gate>> UpdateGate(Guid id, [FromBody] GateServiceFormModel gateForm)
        {
            var userRole = User.FindFirstValue(ClaimTypes.Role);
            try
            {
                var gate = await _gateService.UpdateGateAsync(id, gateForm, userRole);
                if (gate == null)
                {
                    return NotFound();
                }
                return Ok(gate);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteGate/{id}")]
        public async Task<ActionResult> DeleteGate(Guid id)
        {
            var userRole = User.FindFirstValue(ClaimTypes.Role);
            var result = await _gateService.DeleteGateAsync(id, userRole);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
