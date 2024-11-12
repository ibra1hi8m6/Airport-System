namespace AirportSystem.Exceptions
{
    public class AirportSystemException : Exception
    {
        public AirportSystemException(string message) : base(message) { }
        public AirportSystemException(string message, string paramName)
           : base($"{message} (Parameter '{paramName}')") { }
    }
    
}
