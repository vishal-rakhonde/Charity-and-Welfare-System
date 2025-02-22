using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Charity_and_Welfare_System.Data;
using Charity_and_Welfare_System.Models;

[Route("api/donor")]
[ApiController]
public class DonorController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DonorController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Donor Registration
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] DonorRegisterDto donorDto)
    {
        if (await _context.Donors.AnyAsync(d => d.Email == donorDto.Email))
            return BadRequest("Email is already registered.");

        var donor = new Donor
        {
            FullName = donorDto.FullName,
            Email = donorDto.Email,
            PasswordHash = HashPassword(donorDto.Password)
        };

        _context.Donors.Add(donor);
        await _context.SaveChangesAsync();

        return Ok("Donor registered successfully.");
    }

    // Donor Login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] DonorRegisterDto loginDto)
    {
        var donor = await _context.Donors.FirstOrDefaultAsync(d => d.Email == loginDto.Email);

        if (donor == null || donor.PasswordHash != HashPassword(loginDto.Password))
            return Unauthorized("Invalid email or password.");

        return Ok("Donor login successful.");
    }

    // Password Hashing Function
    private string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
