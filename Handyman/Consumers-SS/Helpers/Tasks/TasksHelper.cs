using HandymanUILibrary.API.Consumer.Order.Interface;
using HandymanUILibrary.Models;

namespace Consumers_SS.Helpers.Tasks
{
    public class TasksHelper : ITasksHelper
    {
        ITasksEndPoint? _tasksEndPoint;

        public TasksHelper(ITasksEndPoint? tasksEndPoint)
        {
            _tasksEndPoint = tasksEndPoint;
        }

        //Get a task
        public async Task<TaskModel> GetTask(int id) => await _tasksEndPoint.GetTask(id);
        //Delete a task
        public void DeleteTask(int id) => _tasksEndPoint.DeleteTask(id);
        //Update a task
        public void UpdateTask(TaskModel taskUpdate) => _tasksEndPoint?.UpdateTask(taskUpdate);

        //Cancel a task, put i on stage 11
        public void CancelTask(TaskModel cancellationTaskModel)
        {
            if (cancellationTaskModel != null && cancellationTaskModel.tas_status != 0 && cancellationTaskModel.tas_status < 6)
            {
                cancellationTaskModel.tas_status = 11;
                UpdateTask(cancellationTaskModel);
            }
        }


    }
}
