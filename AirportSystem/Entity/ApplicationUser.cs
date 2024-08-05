using Microsoft.AspNetCore.Identity;
namespace AirportSystem.Entity

{
    public class ApplicationUser : IdentityUser<Guid>
    {
        // Common properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole UserRole { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int GetAge()
        {
            var today = DateTime.Now;
            var age = today.Year - DateOfBirth.Year;
            if (today.DayOfYear < DateOfBirth.DayOfYear)
                age--;
            return age;
        }

    }
    public enum UserRole
    {
        Pilot = 1,
        Passenger = 2,
        TicketCashier = 3,
        Doctor = 4
    }
    public class PassengerUser : ApplicationUser
    {

        public string Email { get; set; }
        public Guid AddressId { get; set; }
        public Address Address { get; set; }

        
    }

    public class PilotUser : ApplicationUser
    {
        
        public int? TotalHours { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<PilotFlight> PilotFlights { get; set; } = new List<PilotFlight>();

        
    }

    public class TicketCashierUser : ApplicationUser
    {
       
    }
    public class DoctorUser : ApplicationUser
    {
        public string DoctorCode { get; set; }
    }
}