namespace AirportSystem.Forms
{
    public class FlightServiceFormModel
    {
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string TakeoffLocation { get; set; }
        public string Destination { get; set; }
        public Guid PlaneId { get; set; }
        public Guid PilotId { get; set; }
        public Guid? DoctorId { get; set; }
    }
    public class FlightResponseModel
    {
        public Guid FlightId { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string TakeoffLocation { get; set; }
        public string Destination { get; set; }
        public Guid PlaneId { get; set; }
        public string PlaneModel { get; set; }
        public int PlanePayload { get; set; }
    }
}
