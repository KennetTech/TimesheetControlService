using Control.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Control.API;

public class ControldataDB : DbContext
{
    public ControldataDB()
    {
        
    }
    public ControldataDB(DbContextOptions options) : base(options) { }
    public DbSet<DashboardEntry> DashboardEntries { get; set; } = null!;
}
