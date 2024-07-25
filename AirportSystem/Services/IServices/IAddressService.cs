using AirportSystem.Entity;
using AirportSystem.Forms;

namespace AirportSystem.Services.IServices
{
    public interface IAddressService
    {
        Task<Address> CreateAddressAsync(AddressServiceFormModel addressForm);
        Task<Address> GetAddressByIdAsync(Guid id);
        Task UpdateAddressAsync(Guid id, AddressServiceFormModel addressForm);
        Task DeleteAddressAsync(Guid id);
    }
}
