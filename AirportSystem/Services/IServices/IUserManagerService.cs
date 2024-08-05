using AirportSystem.Entity;
using AirportSystem.Forms;

namespace AirportSystem.Services.IServices
{
    public interface IUserManagerService
    {
        Task<LoginResponse> LoginAsync(LoginFormModel loginModel);
        Task<UserResponseDto> CreateUserAsync(SignUpFormModel model);
        Task<ApplicationUser> FindByNameAsync(string username);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<IList<string>> GetRolesAsync(ApplicationUser user);
        Task<bool> UpdateUserAsync(Guid userId, UpdateUserFormModel model, Guid requestingUserId);
        Task<bool> DeleteUserByIdAsync(Guid userId, Guid requestingUserId);
        Task<IEnumerable<ApplicationUser>> GetUsersByRoleAsync(string role);
        Task<ApplicationUser> GetUserByIdAsync(Guid userId);
    }
}
