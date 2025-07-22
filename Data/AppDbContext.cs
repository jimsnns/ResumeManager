using Microsoft.EntityFrameworkCore;
using ResumeManager.Models;

namespace ResumeManager.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Degree> Degrees { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* Initialize the database with 
             * a list with the available degrees*/
            modelBuilder.Entity<Degree>().HasData(
                new Degree { Id = 1, Name = "BSc" },
                new Degree { Id = 2, Name = "MSc" },
                new Degree { Id = 3, Name = "PhD" }
            );
        }
    }
}
