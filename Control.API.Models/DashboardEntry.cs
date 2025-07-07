using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.API.Models;

public class DashboardEntry
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Timesheet Timesheet { get; set; }
    public Employee Employee { get; set; }
    public List<GPSdata>? GPSdata { get; set; }
    public Novusdata? Novusdata { get; set; }

}


