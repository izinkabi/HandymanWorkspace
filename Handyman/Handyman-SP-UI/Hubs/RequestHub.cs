using HandymanProviderLibrary.Models;
using Microsoft.AspNetCore.SignalR;

namespace Handyman_SP_UI.Hubs
{
    public class RequestHub : Hub
    {
        public Task PlaceOrder(RequestModel order)
        {
            return Clients.All.SendAsync("ReceiveOrder", order);
        }

    }
}
