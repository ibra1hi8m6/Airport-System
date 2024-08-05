namespace AirportSystem.Entity
{
    public class Address
    {
        public Guid Id { get; set; } 

        public int? House_Number { get; set; }

        public string street { get; set; }

        public string city { get; set; }

        public string Country { get; set; }
    }
}
