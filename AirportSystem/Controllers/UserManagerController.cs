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


        [HttpPost("create-admin")]
        public async Task<IActionResult> CreateAdmin([FromBody] AdminSignUpFormModel adminSignUpModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var adminUser = await _userManagerService.CreateAdminUserAsync(adminSignUpModel);
                return Ok(new { Message = "Admin created successfully", AdminUser = adminUser });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Failed to create admin: {ex.Message}" });
            }
        }


        [HttpPost("signup/passenger")]
        public async Task<IActionResult> SignUpPassenger([FromBody] PassengerSignUpFormModel signUpModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManagerService.CreatePassengerAsync(signUpModel);

            if (user != null)
            {
                return Ok("Sign up successful");
            }

            return BadRequest("Sign up failed");
        }

        [HttpPost("signup/pilot")]
        public async Task<IActionResult> SignUpPilot([FromBody] PilotSignUpFormModel signUpModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManagerService.CreatePilotUserAsync(signUpModel);

            if (user != null)
            {
                return Ok("Sign up successful");
            }

            return BadRequest("Sign up failed");
        }

        [HttpPost("signup/ticketcashier")]
        public async Task<IActionResult> SignUpTicketCashier([FromBody] TicketCashierSignUpFormModel signUpModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManagerService.CreateTicketCashierAsync(signUpModel);

            if (user != null)
            {
                return Ok("Sign up successful");
            }

            return BadRequest("Sign up failed");
        }

        [HttpPost("signup/doctor")]
        public async Task<IActionResult> SignUpDoctor([FromBody] DoctorSignUpFormModel signUpModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManagerService.CreateDoctorUserAsync(signUpModel);

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
