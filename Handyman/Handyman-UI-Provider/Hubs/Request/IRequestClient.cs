namespace Handyman_UI_Provider.Hubs.Request
{
    //A strongly typed Hub
    public interface IRequestClient
    {
        Task ReceiveAvailableBroadcast(string user, string broadcast);

       // Task ReceiveRequestUpdate(string user, RequestModel requestUpdate);
       // Task ReceiveRequest(string user, RequestModel broadcast);  
    }
}