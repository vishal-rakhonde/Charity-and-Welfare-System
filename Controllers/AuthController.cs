using Charity_and_Welfare_System.Data;
using Charity_and_Welfare_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Charity_and_Welfare_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Register Charity
        [HttpPost("register-charity")]
        public async Task<IActionResult> RegisterCharity(Charity charity)
        {
            // Check if email already exists
            if (await _context.Charities.AnyAsync(c => c.Email == charity.Email))
                return BadRequest("Email already registered");

            // Hash the password before storing
            charity.Password = HashPassword(charity.Password);

            _context.Charities.Add(charity);
            await _context.SaveChangesAsync();
            return Ok("Charity registered successfully");
        }

        // Login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _context.Charities.FirstOrDefaultAsync(c => c.Email == request.Email);

            if (user == null || !VerifyPassword(request.Password, user.Password))
                return Unauthorized("Invalid email or password");

            return Ok("Login successful");
        }
        // Hash password
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        // Verify password
        private bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            return HashPassword(enteredPassword) == storedPassword;
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}