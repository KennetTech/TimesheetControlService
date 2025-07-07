using Microsoft.EntityFrameworkCore;
using Novus.API.Models;

namespace Novus.API
{
    public class NovusdataDbContext : DbContext
    {
        public NovusdataDbContext(DbContextOptions<NovusdataDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Novusdatatest> Novusdatatests { get; set; }
    }
}
