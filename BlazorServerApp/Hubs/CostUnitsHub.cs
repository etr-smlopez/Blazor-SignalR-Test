 
using Microsoft.AspNetCore.SignalR; 
using BlazorServerApp.Data; 
using System.Runtime.CompilerServices;
namespace BlazorServerApp.Hubs
{
    public class CostUnitsHub : Hub
    {
        public async Task RefreshCostUnits(List<CostUnitsHub> CostUnits)
        {
            await Clients.All.SendAsync("RefreshCostUnits", CostUnits);
        }
    }
}
