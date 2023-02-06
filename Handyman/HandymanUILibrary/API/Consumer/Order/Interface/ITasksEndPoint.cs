using HandymanUILibrary.Models;
using System.Threading.Tasks;

namespace HandymanUILibrary.API.Consumer.Order.Interface
{
    public interface ITasksEndPoint
    {
        Task DeleteTask(int id);
        Task<TaskModel> GetTask(int id);
        Task UpdateTask(TaskModel taskUpdate);
    }
}