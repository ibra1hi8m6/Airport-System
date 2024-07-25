namespace AirportSystem.Entity
{
    public class Plane
    {
        public Guid Id { get; set; }

        public string Plane_model { get; set; }

        public int Plane_Payload { get; set; }

        public int number_of_seats { get; set; }
    }
}
