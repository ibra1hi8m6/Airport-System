namespace AirportSystem.Entity
{
    public class Flight
    {
        public Guid Id { get; set; }

        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }

        public string Takeoff_Location { get; set; }
        public string Destination { get; set; }
        public TimeSpan FlightDuration { get; set; }


        public Guid PlaneId { get; set; }
        public Plane Plane { get; set; }
        public Guid? DoctorId { get; set; }
        public DoctorUser Doctor { get; set; }
        public ICollection<PilotFlight> PilotFlights { get; set; } = new List<PilotFlight>();



        public void CalculateFlightDuration()
        {
            FlightDuration = ArrivalTime - DepartureTime;
        }
    }

    public class PilotFlight
    {
        public Guid Id { get; set; }
        public Guid PilotUserId { get; set; }
        public PilotUser PilotUser { get; set; }

        public Guid FlightId { get; set; }
        public Flight Flight { get; set; }
    }
}
