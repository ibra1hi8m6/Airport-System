using AirportSystem.Entity;
using AirportSystem.Forms;

namespace AirportSystem.Services.IServices
{
    public interface IUserManagerService
    {
        Task<DoctorUser> CreateDoctorUserAsync(DoctorSignUpFormModel model);
        Task<TicketCashierUser> CreateTicketCashierAsync(TicketCashierSignUpFormModel model);
        Task<PilotUser> CreatePilotUserAsync(PilotSignUpFormModel model);
        Task<PassengerUser> CreatePassengerAsync(PassengerSignUpFormModel model);
       Task<ApplicationUser> CreateAdminUserAsync(AdminSignUpFormModel model);
        Task<ApplicationUser> FindByNameAsync(string username);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<IList<string>> GetRolesAsync(ApplicationUser user);
        Task<bool> DeleteUserByIdAsync(Guid userId);
    }
}
