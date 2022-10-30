using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Interfaces
{
    public interface IOrderData
    {
        IEnumerable<OrderTaskModel> GetConsumerOrderAndTasks(string consumerID);
        void SaveOrder(OrderModel order, List<TaskModel> tasks);
        void UpdateOrder(OrderModel orderUpdate);
        void DeleteOrderAndTasks(string consumerId, int orderId);
    }
}