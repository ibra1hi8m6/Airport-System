using AirportSystem.Entity;
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


        public UserManagerService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ApplicationUser> CreateUserAsync(string username, string email, string password)
        {
            var Role = "Passenger";
            var newUser = new ApplicationUser { UserName = username, Email = email };
            var result = await _userManager.CreateAsync(newUser, password);

            if (!result.Succeeded)
            {
                throw new Exception($"Failed to create user: {string.Join(", ", result.Errors)}");
            }

            await AssignRoleAsync(newUser, Role);
            return newUser;
        }
        public async Task<ApplicationUser> CreateAuthorUserAsync(string username, string email, string password)
        {
            var Role = "Pilot";
            var newUser = new ApplicationUser { UserName = username, Email = email };
            var result = await _userManager.CreateAsync(newUser, password);

            if (!result.Succeeded)
            {
                throw new Exception($"Failed to create user: {string.Join(", ", result.Errors)}");
            }

            await AssignRoleAsync(newUser, Role);
            return newUser;
        }

        public async Task<ApplicationUser> CreateAdminUserAsync(string username, string email, string password)
        {
            var Role = "Admin";
            var newUser = new ApplicationUser { UserName = username, Email = email };
            var result = await _userManager.CreateAsync(newUser, password);

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
