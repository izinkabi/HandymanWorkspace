using HandymanUILibrary.Models;

namespace Consumer_SS.Helpers.Tasks
{
    public interface ITasksHelper
    {
        Task<TaskModel> GetTask(int id);
        void DeleteTask(int id);
        void UpdateTask(TaskModel task);
        void CancelTask(TaskModel task);
    }
}