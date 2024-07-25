﻿namespace AirportSystem.Entity
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





        public void CalculateFlightDuration()
        {
            FlightDuration = ArrivalTime - DepartureTime;
        }
    }
}
