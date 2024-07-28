using AirportSystem.Entity;
using AirportSystem.Entity;
using Microsoft.EntityFrameworkCore;
namespace AirportSystem.Data
{
    public interface IIdentityDbContext
    {
        DbSet<DoctorUser> DoctorUsers { get; set; }
        DbSet<PassengerUser> PassengerUsers { get; set; }
         DbSet<PilotUser> PilotUsers { get; set; }
         DbSet<TicketCashierUser> TicketCashierUsers { get; set; }
         DbSet<Address> Addresses { get; set; }
         DbSet<Flight> Flights { get; set; }

         DbSet<Gate> Gates { get; set; }
         
         DbSet<Plane> Planes { get; set; }
         DbSet<Ticket> Tickets { get; set; }
         
        int SaveChanges();
    }
}
