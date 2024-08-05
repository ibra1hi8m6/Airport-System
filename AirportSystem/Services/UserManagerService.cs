using AirportSystem.Entity;
using AirportSystem.Forms;
using AirportSystem.Services.IServices;
using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirportSystem.Forms.Validate;

namespace AirportSystem.Services
{
    public class UserManagerService : IUserManagerService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IAddressService _addressService;
        private readonly ITokenService _tokenService;
        private readonly IValidateForm _validateForm;

        public UserManagerService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            IAddressService addressService,
            ITokenService tokenService,
            IValidateForm validateForm)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _addressService = addressService;
            _tokenService = tokenService;
            _validateForm = validateForm;
        }


        public async Task<LoginResponse> LoginAsync(LoginFormModel loginModel)
        {
            var result = await _signInManager.PasswordSignInAsync(loginModel.Username, loginModel.Password, loginModel.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(loginModel.Username);
                var token = await _tokenService.GenerateToken(user);
                if (token == null)
                {
                    return null;
                }
                var roles = await _userManager.GetRolesAsync(user);
                return new LoginResponse
                {
                    Token = token,
                    FullName = $"{user.FirstName} {user.LastName}",
                    Role = roles.FirstOrDefault() // Assuming a user has a single role
                };
            }
            return null;
        }
        public async Task<UserResponseDto> CreateUserAsync(SignUpFormModel model)
        {
            var validationMessage = await  _validateForm.ValidateSignUpForm(model);
            if (validationMessage != null)
            {
                throw new Exception(validationMessage);
            }

            UserResponseDto response;
            switch (model.Role)
            {
                case UserRole.Pilot:
                    return await CreatePilotUserAsync(model);

                case UserRole.Passenger:
                    return await CreatePassengerAsync(model);

                case UserRole.TicketCashier:
                    return await CreateTicketCashierAsync(model);

                

                case UserRole.Doctor:
                    return await CreateDoctorUserAsync(model);

                default:
                    throw new Exception("Invalid role");
            }

            var user = await _userManager.FindByIdAsync(response.Id);
            await AssignRoleAsync(user, model.Role.ToString());

            return response;
        }

        public async Task<UserResponseDto> CreateDoctorUserAsync(SignUpFormModel model)
        {
            var newUser = new DoctorUser
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                DoctorCode = model.DoctorCode,
                UserRole = UserRole.Doctor,
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
            {
                throw new Exception($"Failed to create user: {string.Join(", ", result.Errors)}");
            }
            await AssignRoleAsync(newUser, UserRole.Doctor.ToString());

            return new UserResponseDto
            {
                Id = newUser.Id.ToString(),
                FullName = $"{newUser.FirstName} {newUser.LastName}",
                Role=newUser.UserRole.ToString(),
            };
        }

        public async Task<UserResponseDto> CreatePassengerAsync(SignUpFormModel model)
        {
            var address = new Address
            {
                House_Number = model.HouseNumber,
                street = model.Street,
                city = model.City,
                Country = model.Country
            };

            var newUser = new PassengerUser
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                PhoneNumber = model.PhoneNumber,
                UserRole = UserRole.Passenger,
                Address = address
            };


            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
            {
                throw new Exception($"Failed to create user: {string.Join(", ", result.Errors)}");
            }
            await AssignRoleAsync(newUser, UserRole.Passenger.ToString());
            return new UserResponseDto
            {
                Id = newUser.Id.ToString(),
                FullName = $"{newUser.FirstName} {newUser.LastName}",
                Role = newUser.UserRole.ToString(),
            };
        }

        public async Task<UserResponseDto> CreateTicketCashierAsync(SignUpFormModel model)
        {
            var newUser = new TicketCashierUser
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                UserRole = UserRole.TicketCashier,
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
            {
                throw new Exception($"Failed to create user: {string.Join(", ", result.Errors)}");
            }
            await AssignRoleAsync(newUser, UserRole.TicketCashier.ToString());

            return new UserResponseDto
            {
                Id = newUser.Id.ToString(),
                FullName = $"{newUser.FirstName} {newUser.LastName}",
                Role = newUser.UserRole.ToString(),
            };
        }

        public async Task<UserResponseDto> CreatePilotUserAsync(SignUpFormModel model)
        {
            var newUser = new PilotUser
            {
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                TotalHours = model.TotalHours,
                PhoneNumber = model.PhoneNumber,
                DateOfBirth = model.DateOfBirth,
                UserRole = UserRole.Pilot,
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
            {
                throw new Exception($"Failed to create user: {string.Join(", ", result.Errors)}");
            }

            await AssignRoleAsync(newUser, UserRole.Pilot.ToString());
            return new UserResponseDto
            {
                Id = newUser.Id.ToString(),
                FullName = $"{newUser.FirstName} {newUser.LastName}",
                Role = newUser.UserRole.ToString(),
            };
        }

       

        private async Task AssignRoleAsync(ApplicationUser user, string role)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole<Guid>(role));
            }
            await _userManager.AddToRoleAsync(user, role);
        }

     

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }
        public async Task<bool> UpdateUserAsync(Guid userId, UpdateUserFormModel model, Guid requestingUserId)
        {
            var userToUpdate = await _userManager.FindByIdAsync(userId.ToString());
            var requestingUser = await _userManager.FindByIdAsync(requestingUserId.ToString());

            if (userToUpdate == null)
            {
                return false;
            }

            // Check if requesting user is an admin or is trying to update their own account
            var isAdmin = await IsUserInRoleAsync(requestingUser, "Admin");
            if (!isAdmin && userId != requestingUserId)
            {
                throw new UnauthorizedAccessException("You do not have permission to update this user.");
            }

            // Update allowed properties
            userToUpdate.FirstName = model.FirstName ?? userToUpdate.FirstName;
            userToUpdate.LastName = model.LastName ?? userToUpdate.LastName;
            userToUpdate.Email = model.Email ?? userToUpdate.Email;
            userToUpdate.PhoneNumber = model.PhoneNumber ?? userToUpdate.PhoneNumber;

            var result = await _userManager.UpdateAsync(userToUpdate);
            if (!result.Succeeded)
            {
                throw new Exception($"Failed to update user: {string.Join(", ", result.Errors)}");
            }

            // Update password if provided
            if (!string.IsNullOrEmpty(model.NewPassword))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(userToUpdate);
                var passwordResult = await _userManager.ResetPasswordAsync(userToUpdate, token, model.NewPassword);
                if (!passwordResult.Succeeded)
                {
                    throw new Exception($"Failed to update password: {string.Join(", ", passwordResult.Errors)}");
                }
            }

            return true;
        }
        public async Task<bool> IsUserInRoleAsync(ApplicationUser user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }
        public async Task<bool> DeleteUserByIdAsync(Guid userId, Guid requestingUserId)
        {
            var userToDelete = await _userManager.FindByIdAsync(userId.ToString());
            var requestingUser = await _userManager.FindByIdAsync(requestingUserId.ToString());

            if (userToDelete == null)
            {
                return false;
            }

            // Check if requesting user is an admin or is trying to delete their own account
            var isAdmin = await IsUserInRoleAsync(requestingUser, "Admin");
            if (!isAdmin && userId != requestingUserId)
            {
                throw new UnauthorizedAccessException("You do not have permission to delete this user.");
            }

            var result = await _userManager.DeleteAsync(userToDelete);
            return result.Succeeded;
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<ApplicationUser> FindByNameAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Username is required.");
            }

            return await _userManager.FindByNameAsync(username);
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsersByRoleAsync(string role)
        {
            if (string.IsNullOrEmpty(role))
            {
                throw new ArgumentException("Role is required.");
            }

            var usersInRole = await _userManager.GetUsersInRoleAsync(role);
            return usersInRole;
        }

        public async Task<ApplicationUser> GetUserByIdAsync(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentException("User ID is required.");
            }

            return await _userManager.FindByIdAsync(userId.ToString());
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsersByRoleAsync(UserRole role)
        {
            if (!Enum.IsDefined(typeof(UserRole), role))
            {
                throw new ArgumentException("Invalid role.");
            }

            return await _userManager.Users.Where(u => u.UserRole == role).ToListAsync();
        }
    }
}
