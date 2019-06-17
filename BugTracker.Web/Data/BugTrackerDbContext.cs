using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Web.Models
{
    public class BugTrackerDbContext : IdentityDbContext
    {
        public DbSet<Bug> Bugs { get; set; }
        
        public DbSet<Person> People { get; set; }
        
        public BugTrackerDbContext (DbContextOptions<BugTrackerDbContext> options)
            : base(options)
        {
        }
    }
}