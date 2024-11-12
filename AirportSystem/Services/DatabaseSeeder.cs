using AirportSystem.Entity;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace AirportSystem.Services
{
    public class DatabaseSeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public DatabaseSeeder(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAdminUserAsync()
        {
            var adminEmail = "admin@example.com";
            var adminPassword = "Admin123!";
            var adminFirstName = "Admin";
            var adminLastName = "User";
            var adminDateOfBirth = new DateTime(1980, 1, 1);
            var adminUser = await _userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = adminFirstName,
                    LastName = adminLastName,
                    DateOfBirth = adminDateOfBirth
                    // No UserRole property for Admin, as it's handled differently
                };

                var result = await _userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync("Admin"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole<Guid>("Admin"));
                    }

                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
