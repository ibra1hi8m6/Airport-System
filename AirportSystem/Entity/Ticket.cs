namespace AirportSystem.Entity
{
    public class Ticket
    {
        public Guid Id { get; set; }
        
        public string TicketClass { get; set; }

        public int passenger_payload { get; set; }
        public string seatNumber { get; set; }

        public Guid GateId { get; set; }
        public Gate Gate { get; set; }

        public Guid PassengerId { get; set; }
        public PassengerUser Passenger { get; set; }

        public Guid TicketCashierId { get; set; }
        public TicketCashierUser TicketCashier { get; set; }


        public Guid FlightId { get; set; }
        public Flight Flight { get; set; }

        public bool CanUpdate { get; set; } = true;

    }
    public enum TicketClass
    {
        Business = 1,
        Economy
    }
}
