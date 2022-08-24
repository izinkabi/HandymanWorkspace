using Handyman_UI_Provider.Hubs.Models;

namespace Handyman_UI_Provider.Hubs.Order
{
    public interface IOrderClient
    {
        Task ReceiveOrder(string user, OrderModel order);
        
    }
}