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
}
