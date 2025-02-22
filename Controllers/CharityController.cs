using Charity_and_Welfare_System.Data;
using Charity_and_Welfare_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Charity_and_Welfare_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharityController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // Constructor to inject ApplicationDbContext
        public CharityController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/Charity
        [HttpPost]
        public async Task<ActionResult<Charity>> PostCharity(CharityRegisterDto charityDto)
        {
            // Validate the incoming DTO (you can add more validation as needed)
            if (charityDto == null)
            {
                return BadRequest("Invalid charity data.");
            }

            // Map DTO to Charity model
            var charity = new Charity
            {
                CharityName = charityDto.CharityName,
                CharityRegistrationNumber = charityDto.CharityRegistrationNumber,
                CharityLocation = charityDto.CharityLocation,
                Email = charityDto.Email,
                Password = charityDto.Password // In production, make sure to hash the password before storing
            };

            // Add the charity to the database
            _context.Charities.Add(charity);
            await _context.SaveChangesAsync();

            // Return the newly created charity with the generated ID
            return CreatedAtAction(nameof(GetCharity), new { id = charity.Id }, charity);
        }

        // GET: api/Charity/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Charity>> GetCharity(int id)
        {
            var charity = await _context.Charities.FindAsync(id);
            if (charity == null)
            {
                return NotFound();
            }
            return charity;
        }

        // Other methods...
    }
}
