using AirportSystem.Entity;

namespace AirportSystem.Services.IServices
{
    public interface ITokenService
    {
        Task<string> GenerateToken(ApplicationUser user);
    }
}
