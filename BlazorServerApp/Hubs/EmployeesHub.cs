using Microsoft.AspNetCore.SignalR; 
using BlazorServerApp.Data; 
using System.Runtime.CompilerServices;
namespace BlazorServerApp.Hubs
{
    public class EmployeesHub : Hub
    {
        public async Task RefreshEmployees(List<EmployeesHub> Employees)
        {
            await Clients.All.SendAsync("RefreshEmployees", Employees);
        }
    }
}
