using AirportSystem.Entity;

namespace AirportSystem.Services.IServices
{
    public interface IUserManagerService
    {
        Task<ApplicationUser> CreateUserAsync(string username, string email, string password);
        Task<ApplicationUser> CreateAuthorUserAsync(string username, string email, string password);
        Task<ApplicationUser> CreateAdminUserAsync(string username, string email, string password);
        Task<ApplicationUser> FindByNameAsync(string username);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<IList<string>> GetRolesAsync(ApplicationUser user);
        Task<bool> DeleteUserByIdAsync(Guid userId);
    }
}
