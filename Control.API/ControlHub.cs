using Control.API.Models;
using Microsoft.AspNetCore.SignalR;

namespace Control.API;


public class ControlHub : Hub
{
    public async Task SendDashboardEntry(TimesheetTest timesheet)
    {
        await Clients.All.SendAsync("ReceiveDashboardEntry", timesheet);
    }
}
