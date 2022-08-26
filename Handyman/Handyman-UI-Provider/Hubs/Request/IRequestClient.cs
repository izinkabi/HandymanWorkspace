using Handyman_UI_Provider.Hubs.Models;

namespace Handyman_UI_Provider.Hubs.Request
{
    //A strongly typed Hub
    public interface IRequestClient
    {
        //Task ReceiveAvailableBroadcast(string user, string broadcast);

        //Provider's method to listen
        Task ReceiveRequestUpdate(RequestModel order);
        //Consumer's method to listen
        Task ReceiveOrder(string status);

        //Task ReceiveRequest(string user, RequestModel broadcast);

    }
}