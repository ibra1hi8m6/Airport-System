namespace AirportSystem.Entity
{
    public class Passenger
    {
        public Guid Id { get; set; }

        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public DateTime Date_of_birth { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }

        public Guid AddressId { get; set; }
        public Address Address { get; set; }
    }
}
