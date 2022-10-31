using HandymanUILibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HandymanUILibrary.API.Consumer.Tasks;

public interface ITaskEndPoint
{
    Task PostTask(List<TaskModel> todoList);
    Task<List<TaskModel>> GetTaskListByOrderId(int Id);
    Task UpdateTask(TaskModel todoUpdate);
}