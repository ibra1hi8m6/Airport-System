using AirportSystem.Entity;
using AirportSystem.Forms;
using AirportSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GateController : ControllerBase
    {
        private readonly IGateService _gateService;

        public GateController(IGateService gateService)
        {
            _gateService = gateService;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Gate>> CreateGate([FromBody] GateServiceFormModel gateForm)
        {

            try
            {
                var gate = await _gateService.CreateGateAsync(gateForm);
                return CreatedAtAction(nameof(GetGateById), new { id = gate.Id }, gate);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Gate>> GetGateById(Guid id)
        {
            var gate = await _gateService.GetGateByIdAsync(id);
            if (gate == null)
            {
                return NotFound();
            }
            return Ok(gate);
        }

        [HttpGet]
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
        [HttpPut("{id}")]
        public async Task<ActionResult<Gate>> UpdateGate(Guid id, [FromBody] GateServiceFormModel gateForm)
        {
            var gate = await _gateService.UpdateGateAsync(id, gateForm);
            if (gate == null)
            {
                return NotFound();
            }
            return Ok(gate);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGate(Guid id)
        {
            var result = await _gateService.DeleteGateAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
