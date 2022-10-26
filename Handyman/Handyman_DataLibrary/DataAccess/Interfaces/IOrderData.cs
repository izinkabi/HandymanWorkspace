using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Interfaces
{
    public interface IOrderData
    {
        List<TaskModel> GetConsumerOrderAndTasks(string consumerID);
        void InsertOrder();
    }
}