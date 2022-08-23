

using Microsoft.AspNetCore.SignalR;

namespace Handyman_UI_Provider.Hubs
{
    public class ChatHub : Hub
    {
        public Task SendMessage(string user, string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", user, message);    
        }
    }
}
