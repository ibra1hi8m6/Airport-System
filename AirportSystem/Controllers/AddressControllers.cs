using AirportSystem.Entity;
using AirportSystem.Forms;
using AirportSystem.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AirportSystem.Controllers
{
    [ApiController]
    [Route("api/addresses")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpPost("CreateAddress")]
        public async Task<IActionResult> CreateAddress([FromBody] AddressServiceFormModel addressForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdAddress = await _addressService.CreateAddressAsync(addressForm);
            return Ok(createdAddress);
        }

        [HttpGet("GetAddress/{id}", Name = "GetAddress")]
        public async Task<IActionResult> GetAddress(Guid id)
        {
            var address = await _addressService.GetAddressByIdAsync(id);
            if (address == null)
            {
                return NotFound();
            }
            return Ok(address);
        }

        [HttpPut("UpdateAddress/{id}")]
        public async Task<IActionResult> UpdateAddress(Guid id, [FromBody] AddressServiceFormModel addressForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingAddress = await _addressService.GetAddressByIdAsync(id);
            if (existingAddress == null)
            {
                return NotFound();
            }

            await _addressService.UpdateAddressAsync(id, addressForm);

            return NoContent();
        }

        [HttpDelete("DeleteAddress/{id}")]
        public async Task<IActionResult> DeleteAddress(Guid id)
        {
            var existingAddress = await _addressService.GetAddressByIdAsync(id);
            if (existingAddress == null)
            {
                return NotFound();
            }

            await _addressService.DeleteAddressAsync(id);
            return NoContent();
        }

        [HttpGet("GetAllAddresses")]
        public async Task<IActionResult> GetAllAddresses()
        {
            var addresses = await _addressService.GetAllAddressesAsync();
            return Ok(addresses);
        }
    }
}
