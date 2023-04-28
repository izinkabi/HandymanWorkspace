using Microsoft.AspNetCore.SignalR;
namespace Handyman_SP_UI.Hubs;

public class NegotiationHub : Hub
{
    //Server Side SendOffer 
    public Task SendOffer(string user, float offer)
    {
        return Clients.All.SendAsync("ReceiveOffer", user, offer);
    }

}
