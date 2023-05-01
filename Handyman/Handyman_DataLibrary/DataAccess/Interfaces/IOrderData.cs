using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Interfaces
{
    public interface IOrderData
    {
        IEnumerable<OrderModel> GetOrders(string consumerID);
        int InsertOrder(OrderModel order);
        void UpdateOrder(OrderModel orderUpdate);
        void DeleteOrderAndTasks(string consumerId, int orderId);

    }
}