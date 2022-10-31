using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Interfaces
{
    public interface IOrderData
    {
        IEnumerable<OrderModel> GetConsumerOrderAndTasks(string consumerID);
        void SaveOrder(OrderModel order);
        void UpdateOrder(OrderModel orderUpdate);
        void DeleteOrderAndTasks(string consumerId, int orderId);
    }
}