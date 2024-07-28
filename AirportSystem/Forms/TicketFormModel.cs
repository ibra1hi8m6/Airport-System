using AirportSystem.Entity;

namespace AirportSystem.Forms
{
    public class TicketFormModel
    {
        public TicketClass TicketClass { get; set; }
        public int PassengerPayload { get; set; }
        public string SeatNumber { get; set; }
        public Guid GateId { get; set; }
        public Guid PassengerId { get; set; }
        public Guid TicketCashierId { get; set; }
        public Guid FlightId { get; set; }
    }

    public class TicketUpdateFormModel
    {
        public string TicketClass { get; set; }
        public int PassengerPayload { get; set; }
        public string SeatNumber { get; set; }
        public Guid GateId { get; set; }
        public Guid FlightId { get; set; }
    }
    public class TicketResponseModel
    {
        public Guid TicketId { get; set; }
        public string TicketClass { get; set; }
        public int PassengerPayload { get; set; }
        public string SeatNumber { get; set; }
        public Guid GateId { get; set; }
        public Guid PassengerId { get; set; }
        public string PassengerName { get; set; }
        public Guid TicketCashierId { get; set; }
        public Guid FlightId { get; set; }
        public string FlightDetails { get; set; }
        public Guid PlaneId { get; set; }
    }
}
