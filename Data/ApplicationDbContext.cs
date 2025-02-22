using Microsoft.EntityFrameworkCore;
using Charity_and_Welfare_System.Models;

namespace Charity_and_Welfare_System.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Charity> Charities { get; set; }
        public DbSet<Donor> Donors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
        }
    }
}
