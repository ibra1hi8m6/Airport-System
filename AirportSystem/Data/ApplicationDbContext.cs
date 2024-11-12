using AirportSystem.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AirportSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>, IIdentityDbContext
    {
        public DbSet<DoctorUser> DoctorUsers { get; set; }
        public DbSet<PassengerUser> PassengerUsers { get; set; }
        public DbSet<PilotUser> PilotUsers { get; set; }
        public DbSet<TicketCashierUser> TicketCashierUsers { get; set; }
        public DbSet<PilotFlight> PilotFlights { get; set; }
        
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Flight> Flights { get; set; }

        public DbSet<Gate> Gates { get; set; }
        
        public DbSet<Plane> Planes { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public override int SaveChanges()
        {
            
            return base.SaveChanges();
        }
       
    }
}
