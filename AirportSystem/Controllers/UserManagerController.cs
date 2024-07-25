using AirportSystem.Data;
using AirportSystem.Entity;
using AirportSystem.Forms;
using AirportSystem.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using AirportSystem.Services.IServices;

namespace AirportSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserManagerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserManagerService _userManagerService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;

        public UserManagerController(ApplicationDbContext context, IUserManagerService userManagerService, SignInManager<ApplicationUser> signInManager, ITokenService tokenService)
        {
            _userManagerService = userManagerService;
            _signInManager = signInManager;
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginFormModel loginModel)
        {
            var result = await _signInManager.PasswordSignInAsync(loginModel.Username, loginModel.Password, loginModel.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var user = await _userManagerService.FindByNameAsync(loginModel.Username);
                var token = _tokenService.GenerateToken(user);
                return Ok(new { Token = token });
            }
            return Unauthorized("Invalid login attempt");
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpFormModel signUpModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ApplicationUser user = null;

            switch (signUpModel.Role)
            {
                case UserRole.Admin:
                    user = await _userManagerService.CreateAdminUserAsync(signUpModel.Username, signUpModel.Email, signUpModel.Password);
                    break;
                case UserRole.Pilot:
                    user = await _userManagerService.CreateAuthorUserAsync(signUpModel.Username, signUpModel.Email, signUpModel.Password);
                    break;
                case UserRole.Passenger:
                default:
                    user = await _userManagerService.CreateUserAsync(signUpModel.Username, signUpModel.Email, signUpModel.Password);
                    break;
            }

            if (user != null)
            {
                return Ok("Sign up successful");
            }

            return BadRequest("Sign up failed");
        }


        // New API to get all users
        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManagerService.GetAllUsersAsync();
            var usersWithRoles = new List<object>();

            foreach (var user in users)
            {
                var roles = await _userManagerService.GetRolesAsync(user);

                var userWithRoles = new
                {
                    user.Id,
                    user.UserName,
                    user.Email,
                    Roles = roles
                };

                usersWithRoles.Add(userWithRoles);
            }

            return Ok(usersWithRoles);
        }

        // New API to delete user by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _userManagerService.DeleteUserByIdAsync(id);
            if (result)
            {
                return NoContent();
            }
            return NotFound("User not found");
        }
    }
}
