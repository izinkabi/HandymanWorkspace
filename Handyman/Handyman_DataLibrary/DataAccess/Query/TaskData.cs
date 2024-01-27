using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Internal.DataAccess;
using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Query
{
    public class TaskData : ITaskData
    {
        ISQLDataAccess? _dataAccess;

        public TaskData(ISQLDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        IList<TaskModel>? Tasks { get; set; }

        /// <summary>
        /// Get a task by an id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TaskModel? GetTask(int id)
        {
            TaskModel task = null;
            if (id != 0)
            {
                try
                {
                    var t = _dataAccess.LoadData<TaskModel, dynamic>("Request.spTaskLookUp", new { taskId = id }, "Handyman_DB").FirstOrDefault();
                    task = t;
                }
                catch (Exception)
                {
                    throw;
                    return null;
                }

            }
            if (task != null) { return task; }
            return null;
        }

        /// <summary>
        /// Get a list of tasks for the given order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>

        public IList<TaskModel> GetTasks(int orderId)
        {
            try
            {
                List<TaskModel> tasks = _dataAccess.LoadData<TaskModel, dynamic>("Delivery.spTaskLookUpByOrder", new { orderId = orderId }, "Handyman_DB");
                return tasks;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Update a task
        /// </summary>
        /// <param name="taskUpdate"></param>
        /// <exception cref="Exception"></exception>
        public void UpdateTask(TaskModel taskUpdate)
        {

            try
            {
                if (taskUpdate != null)
                {
                    var result = _dataAccess.SaveData("Request.spTaskUpdate",
                        new
                        {
                            task_id = taskUpdate.task_id,
                            tas_date_started = taskUpdate.tas_date_started,
                            tas_date_finished = taskUpdate.tas_date_finished,
                            tas_duration = taskUpdate.tas_duration,
                            tas_status = taskUpdate.tas_status

                        }, "Handyman_DB");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Insert a task to the database
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public int InsertTask(TaskModel task)
        {
            int taskId = 0;
            try
            {
                if (task != null)
                {
                    task.tas_date_started = DateTime.Now;
                    task.tas_date_finished = DateTime.Now;

                    taskId = _dataAccess.LoadData<int, dynamic>("Request.spTaskInsert", new
                    {
                        dateStarted = task.tas_date_finished,
                        title = task.tas_title,
                        duration = task.tas_duration,
                        status = task.tas_status,
                        description = task.tas_description,
                        serviceId = task.tas_service_id,
                        dateFinished = task.tas_date_finished,

                    }, "Handyman_DB").First();
                }
                return taskId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete the task and it's links
        /// </summary>
        /// <param name="id"></param>
        public void DeleteTask(int id)
        {
            try
            {
                _dataAccess.SaveData("Request.DeleteTask", new { taskId = id }, "Handyman_DB");
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get the price of the given task id
        public PriceModel GetTaskPrice(int TaskId)
        {
            try
            {
                PriceModel taskPrice = _dataAccess.LoadData<PriceModel, dynamic>("Request.spTaskPriceLookUp", new { TaskId = TaskId }, "Handyman_DB").FirstOrDefault() ?? new PriceModel();
                return taskPrice;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        //Insert a new TaskPrice
        public void InsertTaskPrice(int taskId, int priceId)
        {
            try
            {
                _dataAccess.SaveData<dynamic>("Request.spTaskPriceInsert",
                    new
                    {
                        TaskId = taskId,
                        PriceId = priceId
                    },
                    "Handyman_DB");
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
