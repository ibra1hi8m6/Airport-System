namespace AirportSystem.Entity
{
    public class Plane
    {
        public Guid Id { get; set; }

        public string Plane_model { get; set; }

        public int Plane_Payload { get; set; }

        public int seats_Economy { get; set; }

        public int seats_Business { get; set; }
    }
    public class PlaneResponseModel
    {
        public Guid PlaneId { get; set; }
        public string PlaneModel { get; set; }
        public int PlanePayload { get; set; }
        public int SeatsEconomy { get; set; }
        public int SeatsBusiness { get; set; }
        public string Message { get; set; }
    }
}
