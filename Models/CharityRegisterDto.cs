namespace Charity_and_Welfare_System.Models
{
    public class CharityRegisterDto
    {
        public string CharityName { get; set; }
        public string CharityRegistrationNumber { get; set; }
        public string CharityLocation { get; set; }
        public string Email { get; set; }  // Registered email used as User ID
        public string Password { get; set; }
    }
}
