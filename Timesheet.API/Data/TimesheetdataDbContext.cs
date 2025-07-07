using Microsoft.EntityFrameworkCore;


namespace Timesheet.API.Data;

public class TimesheetdataDbContext : DbContext
{
    public TimesheetdataDbContext(DbContextOptions<TimesheetdataDbContext> options):
        base(options)
    {
        Database.EnsureCreated();   
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<TimesheetModel> Timesheet {  get; set; }
}
