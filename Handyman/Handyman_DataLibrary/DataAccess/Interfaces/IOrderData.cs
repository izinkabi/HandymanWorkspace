using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Interfaces
{
    public interface IOrderData
    {
        IEnumerable<RequestModel> GetOrders(string consumerID);
        int InsertOrder(RequestModel order);
        void UpdateOrder(RequestModel orderUpdate);
        void DeleteOrderAndTasks(string consumerId, int orderId);

    }
}