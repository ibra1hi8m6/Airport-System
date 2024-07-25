using AirportSystem.Entity;

namespace AirportSystem.Forms
{
   

    public class LoginFormModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
    public class SignUpFormModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }
}
