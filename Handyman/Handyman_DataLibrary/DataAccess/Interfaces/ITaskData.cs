using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Interfaces
{
    public interface ITaskData
    {
        TaskModel? GetTask(int id);
        IList<TaskModel> GetTasks(int orderId);
        int InsertTask(TaskModel task);
        void UpdateTask(TaskModel taskUpdate);
        void DeleteTask(int id);
        PriceModel GetTaskPrice(int TaskId);
        void InsertTaskPrice(int taskId, int priceId);
    }
}