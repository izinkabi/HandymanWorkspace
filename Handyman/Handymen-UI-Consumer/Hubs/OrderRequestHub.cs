using HandymanUILibrary.Models;
using Handymen_UI_Consumer.Models;
using Microsoft.AspNetCore.SignalR;

namespace Handymen_UI_Consumer.Hubs
{
    public class OrderRequestHub : Hub
    {
        //This will be our collection of service-providers and their addresses
        Dictionary<ServiceProviderModel,ProviderAddress>? availableServiceProviders;
        
        //public Task BroadcastRequest(string user, Order order)
        //{
        //    return Clients.All.SendAsync("ReceiveRequest", user, order);
        //}

        //Send the request to the specified service provider
        public Task BroadcastRequest(string user, string message)
        {
            //for each of the service-providers broadcast a new request
            return Clients.User(user).SendAsync("ReceiveBraodcast", message);
        }

        //Update the order's status / stage
        public Task UpdateOrder(string user, string status)
        {
            return Clients.User(user).SendAsync("ReceiveOrderUpdate", status);
        }


       //Here we filter service-providers to the most specific group to broadcast to
        private async Task FilterProviders(int serviceID)
        {
            //Get the providers of the given service
            //Here we call the Google api to sort the providers' addresses by closest
            //Then return a list of service-providers sorted by closest 
        }

    }
}
