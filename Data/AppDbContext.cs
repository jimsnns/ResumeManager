using Microsoft.EntityFrameworkCore;
using ResumeManager.Models;

namespace ResumeManager.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Degree> Degrees { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    }
}
