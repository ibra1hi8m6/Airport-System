using AirportSystem.Data;
using AirportSystem.Entity;
using AirportSystem.Services.IServices;
using AirportSystem.Forms;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AirportSystem.Services
{
    public class AddressService : IAddressService
    {
        private readonly ApplicationDbContext _context;

        public AddressService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Address> CreateAddressAsync(AddressServiceFormModel addressForm)
        {
            var address = new Address
            {
                
                House_Number = addressForm.HouseNumber,
                street = addressForm.Street,
                city = addressForm.City,
                Country = addressForm.Country
            };

            await _context.Addresses.AddAsync(address);
            await _context.SaveChangesAsync();
            return address;
        }

        public async Task<Address> GetAddressByIdAsync(Guid id)
        {
            return await _context.Addresses.FindAsync(id);
        }

        public async Task UpdateAddressAsync(Guid id, AddressServiceFormModel addressForm)
        {
            var address = await GetAddressByIdAsync(id);

            if (address != null)
            {
                address.House_Number = addressForm.HouseNumber;
                address.street = addressForm.Street;
                address.city = addressForm.City;
                address.Country = addressForm.Country;

                _context.Addresses.Update(address);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAddressAsync(Guid id)
        {
            var address = await GetAddressByIdAsync(id);
            if (address != null)
            {
                _context.Addresses.Remove(address);
                await _context.SaveChangesAsync();
            }
        }
    }
}


