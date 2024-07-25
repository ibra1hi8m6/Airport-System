namespace AirportSystem.Entity
{
    public class Pilot
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int TotalHours { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }


        public Guid FlightId { get; set; }
        public Flight Flight { get; set; }
        


        public int GetAge()
        {
            var today = DateTime.Now;
            var age = today.Year - DateOfBirth.Year;
            // Adjust for leap year and days past birthday
            if (today.DayOfYear < DateOfBirth.DayOfYear)
                age--;
            return age;
        }
    }
}
