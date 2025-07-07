namespace Novus.API.Models;

public class Novusdata
{
    public int ScheduleID { get; set; }

    public int RunID { get; set; }

    public int Activity { get; set; }

    public int EstimatedArrive { get; set; }

    public int EstimatedBegin { get; set; }

    public int EstimatedDepart { get; set; }

    public int? ActualArrive { get; set; }

    public int? ActualDepart { get; set; }

    public int ScheduleStatus { get; set; }
}
