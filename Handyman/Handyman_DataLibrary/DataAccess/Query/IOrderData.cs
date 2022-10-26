using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Query
{
    public interface IOrderData
    {
        List<TaskModel> GetConsumerOrderAndTasks(string consumerID);
        void SaveOrder(OrderModel order, List<TaskModel> tasks);
        void UpdateOrder(OrderModel orderUpdate);
    }
}