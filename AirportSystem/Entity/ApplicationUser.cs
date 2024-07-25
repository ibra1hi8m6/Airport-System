using Microsoft.AspNetCore.Identity;
namespace AirportSystem.Entity

{
    public class ApplicationUser : IdentityUser<Guid>
    {
        // Common properties
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

    }
    public enum UserRole
    {
        Pilot=1,
        Passenger,
        TicketCashier,
        Admin
    }
    public class PassengerUser : ApplicationUser
    {
      
        public Guid AddressId { get; set; }
        public Address Address { get; set; }
    }

    public class PilotUser : ApplicationUser
    {
        
        public int TotalHours { get; set; }
        public string PhoneNumber { get; set; }
        
        public Guid FlightId { get; set; }
        public Flight Flight { get; set; }

        public int GetAge()
        {
            var today = DateTime.Now;
            var age = today.Year - DateOfBirth.Year;
            if (today.DayOfYear < DateOfBirth.DayOfYear)
                age--;
            return age;
        }
    }

    public class TicketCashierUser : ApplicationUser
    {
        public string CashierName { get; set; }
    }
}