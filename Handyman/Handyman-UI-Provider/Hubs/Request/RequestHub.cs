using Handyman_UI_Provider.Hubs.ServiceDelivery;
using Microsoft.AspNetCore.SignalR;

namespace Handyman_UI_Provider.Hubs.Request
{
    //Service providers invoke / broadcast and listen to methods of this hub
    //while consumers only listen 
    public class RequestHub : Hub<IRequestClient>
    {
        private IServiceDelivery _serviceDelivery;

        public RequestHub(IServiceDelivery serviceDelivery)
        {
            _serviceDelivery = serviceDelivery;
        }


        //Broadcasting the availability of the provider and listenin for the response

        public async Task SendAvailBroadcast(string user, string broadcast)
        => await Clients.All.ReceiveAvailableBroadcast(user, broadcast);

        public async Task SendAvailBroadcastCaller(string user, string broadcast)
         => await Clients.Caller.ReceiveAvailableBroadcast(user, broadcast);

        public async Task SendBroadcastToGroup(string user, string broadcast)
         => await Clients.Group("Service Providers").ReceiveAvailableBroadcast(user, broadcast);

        //Direct communication
        // [HubMethodName("SendUpdatedRequestToConsumer")] named hub methods
        //public async Task UpdateRequest(string user, string message)
        // => await Clients.User(user).ReceiveUpdatedOrder(user, message);

        //public async Task SendRequestToConsumer(string user, string broadcast)
        //=> await Clients.Caller.ReceiveAvailableBroadcast(user, broadcast);

       
    }
}
