using AirportSystem.Entity;
using AirportSystem.Forms;
using AirportSystem.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AirportSystem.Services
{
   

    public class UserManagerService : IUserManagerService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IAddressService _addressService;

        public UserManagerService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager, IAddressService addressService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _addressService = addressService;
        }
        public async Task<DoctorUser> CreateDoctorUserAsync(DoctorSignUpFormModel model)
        {
            var newUser = new DoctorUser
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                DoctorCode = model.DoctorCode
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
            {
                throw new Exception($"Failed to create user: {string.Join(", ", result.Errors)}");
            }

            await AssignRoleAsync(newUser, "Doctor");
            return newUser;
        }

        public async Task<PassengerUser> CreatePassengerAsync(PassengerSignUpFormModel model)
        {
            var newUser = new PassengerUser
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                PhoneNumber = model.PhoneNumber
            };

            // Create and save the address
            var addressForm = new AddressServiceFormModel
            {
                HouseNumber = model.HouseNumber,
                Street = model.Street,
                City = model.City,
                Country = model.Country
            };
            var address = await _addressService.CreateAddressAsync(addressForm);
            newUser.AddressId = address.Id;
            newUser.Address = address;

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
            {
                throw new Exception($"Failed to create user: {string.Join(", ", result.Errors)}");
            }

            await AssignRoleAsync(newUser, "Passenger");
            return newUser;
        }
        public async Task<TicketCashierUser> CreateTicketCashierAsync(TicketCashierSignUpFormModel model)
        {
            var newUser = new TicketCashierUser
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
            {
                throw new Exception($"Failed to create user: {string.Join(", ", result.Errors)}");
            }

            await AssignRoleAsync(newUser, "TicketCashier");
            return newUser;
        }
        public async Task<PilotUser> CreatePilotUserAsync(PilotSignUpFormModel model)
        {
            var newUser = new PilotUser
            {
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                TotalHours = model.TotalHours,
                PhoneNumber = model.PhoneNumber,
                
                DateOfBirth = model.DateOfBirth
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
            {
                throw new Exception($"Failed to create user: {string.Join(", ", result.Errors)}");
            }

            await AssignRoleAsync(newUser, "Pilot");
            return newUser;
        }

        public async Task<ApplicationUser> CreateAdminUserAsync(AdminSignUpFormModel model)
        {
            var Role = "Admin";
            var newUser = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth
            };
            var result = await _userManager.CreateAsync(newUser, model.Password);
            if (!result.Succeeded)
            {
                throw new Exception($"Failed to create user: {string.Join(", ", result.Errors)}");
            }

            await AssignRoleAsync(newUser, Role);
            return newUser;
        }

        private async Task AssignRoleAsync(ApplicationUser user, string role)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole<Guid>(role));
            }
            await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<ApplicationUser> FindByNameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }
        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<bool> DeleteUserByIdAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return false;
            }

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }
        public async Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }
    }


}
