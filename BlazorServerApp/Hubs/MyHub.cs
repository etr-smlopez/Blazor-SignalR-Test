 
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

 
namespace BlazorServerApp.Hubs
{
    public class MyHub : Hub
    {
        [HubMethodName("sendMessages")]
        public static void SendMessages()
        {
               // IHubContext context = GlobalHost.ConnectionManager.GetHubContext<MyHub>();
               //context.Clients.All.updateMessages();
        }

        public async Task SendUpdate(string message)
        {
            await Clients.All.SendAsync("ReceiveUpdate", message);
        }
    }
}
