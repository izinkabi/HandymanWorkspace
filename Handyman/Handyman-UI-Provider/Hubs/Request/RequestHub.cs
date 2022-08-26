using Handyman_UI_Provider.Hubs.Models;
using Handyman_UI_Provider.Hubs.ServiceDelivery;
using Microsoft.AspNetCore.SignalR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        //[HubMethodName("SendAvailBroadcast")]
        //public async Task SendAvailBroadcast(string user, string broadcast)
        //=> await Clients.All.ReceiveAvailableBroadcast(user, broadcast);

        // [HubMethodName("SendAvailBroadcastCaller")]
        //public async Task SendAvailBroadcastToCaller(string user, string broadcast)
        //{

        //    await Clients.Caller.ReceiveAvailableBroadcast(user, broadcast);
        //}
        //[HubMethodName("SendBroadcastToGroup")]
        //public async Task SendBroadcastToGroup(string user, string broadcast)
        // => await Clients.Group("Consumer").ReceiveAvailableBroadcast(user, broadcast);

        //Direct communication
        // [HubMethodName("SendUpdatedRequestToConsumer")] named hub methods
        public async Task SendRequestToConsumer(string user, string status)
        {
            
            await Clients.User(user).ReceiveOrder(status);
        }
        //public async Task SendRequestToConsumer(string user, string broadcast)
        //=> await Clients.Caller.ReceiveAvailableBroadcast(user, broadcast);


    }
}
