namespace Charity_and_Welfare_System.Models
{
    public class Donor
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; } // Email used for login
        public string PasswordHash { get; set; } // Store hashed password
    }
}
