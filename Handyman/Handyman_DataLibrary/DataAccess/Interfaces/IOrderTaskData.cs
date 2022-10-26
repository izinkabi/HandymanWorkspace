using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Interfaces
{
    public interface IOrderTaskData
    {
        void InsertTask(TaskModel task);
        void UpdateTask(TaskModel taskUpdate);
        int GetNewTask(int orderId);
    }
}