using HandymanUILibrary.API.Consumer.Order.Interface;
using HandymanUILibrary.Models;
using System;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace HandymanUILibrary.API.Consumer.Order.Implementation
{
    public class TasksEndPoint : ITasksEndPoint
    {
        IAPIHelper _apiHelper;

        private TaskModel taskModel;
        public TasksEndPoint(IAPIHelper aPIHelper)
        {
            _apiHelper = aPIHelper;
        }


        public async Task<TaskModel> GetTask(int id)
        {

            try
            {
                if (id != 0)
                {
                    var t = await _apiHelper.ApiClient?.GetFromJsonAsync<TaskModel>($"/api/Tasks/GetTask?id={id}");
                    taskModel = t;
                    if (taskModel != null)
                    {
                        taskModel.Id = id;
                        return taskModel;
                    }
                }
                return taskModel;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);

            }
        }

        //Delete a task
        public async Task DeleteTask(int id)
        {

            try
            {
                if (id != 0)
                {
                    await _apiHelper.ApiClient.DeleteAsync($"/api/Tasks/Delete?id={id}");

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);

            }
        }

        //Update Task
        public async Task UpdateTask(TaskModel taskUpdate)
        {
            try
            {
                if (taskUpdate != null)
                {
                    await _apiHelper.ApiClient.PutAsJsonAsync<TaskModel>("/api/Tasks/Update", taskUpdate);
                }
            }
            catch (Exception)
            {
                _apiHelper = null;
                throw;
            }
        }
    }
}
