using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace BlazorServerApp.Hubs
{
    public class UpdateHub : Hub
    {
        public async Task SendUpdate(string message)
        {
            await Clients.All.SendAsync("ReceiveUpdate", message);
        }
    }
}
