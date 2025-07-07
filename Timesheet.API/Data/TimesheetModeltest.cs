namespace Timesheet.API.Data;

public class TimesheetModeltest
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string EmployeeId { get; set; } = string.Empty;
    public string EmployeeName { get; set; } = string.Empty;

    public DateTime Date { get; set; } = DateTime.Today;

    public DateTime GarageOut { get; set; }
    public DateTime FirstPickup { get; set; }
    public DateTime LastDropoff { get; set; }
    public DateTime GarageIn { get; set; }

    public int PauseSeconds { get; set; }

    public int TotalSeconds { get; set; }

    public bool ApprovedByEmployee { get; set; }

    public string vehicleId { get; set; } = string.Empty;
}
