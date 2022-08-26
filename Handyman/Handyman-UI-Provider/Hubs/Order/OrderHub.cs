using Handyman_UI_Provider.Hubs.Models;
using Handyman_UI_Provider.Hubs.ServiceDelivery;
using Microsoft.AspNetCore.SignalR;

namespace Handyman_UI_Provider.Hubs.Order
{
    //Consumers invoke / broadcast and listen to methods of this hub
    //but the provider only listens to the 
    public class OrderHub : Hub<IOrderClient>//strongly typed hubs 
    {
        private IServiceDelivery _serviceDelivery;
       
        public OrderHub(IServiceDelivery serviceDelivery)
        {
            _serviceDelivery = serviceDelivery;
        }

        //[HubMethodName("SendOrder")]
        public async Task SendOrderToProvider(string user, OrderModel order)
        {
            RequestModel request = new();
            request.OrderProperty = order;
            await Clients.User(user).ReceiveRequest(request);
        }

        //[HubMethodName("SendOrderToCaller")]
        //public async Task SendOrderToCaller(string user, OrderModel order)
        //{
        //    await Clients.Caller.ReceiveOrder(user, order);
        //}
        ////[HubMethodName("SendOrderToGroup")]
        //public async Task SendOrderToGroup(string user, OrderModel order)
        //{
        //     await Clients.Group("Service Providers").ReceiveOrder(user, order);
        //}

    }
}
