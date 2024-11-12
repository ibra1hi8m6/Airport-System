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
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AirportSystem.Controllers
{
    [ApiController]
    [Route("api/UserManagement")]
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

        [HttpPost("LoginUser")]
        public async Task<IActionResult> Login([FromBody] LoginFormModel loginModel)
        {
            var userResponse = await _userManagerService.LoginAsync(loginModel);

            if (userResponse == null)
            {
                return Unauthorized("Invalid login attempt");
            }

            return Ok(userResponse);
        }

        [HttpPost("SignUpNewUser")]
        public async Task<IActionResult> SignUp([FromBody] SignUpFormModel model)
        {
            try
            {
                var userResponse = await _userManagerService.CreateUserAsync(model);
                return Ok(userResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("GetAllRoles")]
        public IActionResult GetRoles()
        {
            var roles = Enum.GetValues(typeof(UserRole));
            var roleList = new List<object>();

            foreach (var role in roles)
            {
                roleList.Add(new
                {
                    Name = role.ToString(),
                    Value = (int)role
                });
            }

            return Ok(roleList);
        }

        [HttpGet("GetUsersByRole/{role}")]
        public async Task<IActionResult> GetUsersByRole(UserRole role)
        {
            // Convert UserRole enum to string
            var roleName = role.ToString();
            var users = await _userManagerService.GetUsersByRoleAsync(roleName);
            return Ok(users);
        }
        /* // New API to get all users
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
         }*/
        [Authorize]
        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserFormModel model)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                var result = await _userManagerService.UpdateUserAsync(id, model, userId);
                if (result)
                {
                    return Ok("User updated successfully");
                }
                return NotFound("User not found");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [Authorize]
        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                var result = await _userManagerService.DeleteUserByIdAsync(id, userId);
                if (result)
                {
                    return NoContent();
                }
                return NotFound("User not found");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
        }

    }
}
