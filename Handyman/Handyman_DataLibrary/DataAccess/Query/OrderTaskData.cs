using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Internal.DataAccess;
using Handyman_DataLibrary.Models;


namespace Handyman_DataLibrary.DataAccess.Query
{
    public class OrderTaskData : IOrderTaskData
    {
        ISQLDataAccess _dataAccess;

        public OrderTaskData(ISQLDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        //Update Task
        public void UpdateTask(TaskModel taskUpdate)
        {
            try
            {
                var result = _dataAccess.SaveData<TaskModel>("Request.spTaskUpdate", taskUpdate, "Handyman_DB");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Insert New Task
        public int InsertTask(TaskModel task)
        {
            int taskId = 0;
            try
            {
                task.dateStarted = DateTime.Now;
                task.dateFinished = DateTime.Now;
                 taskId = _dataAccess.LoadData<int, dynamic>("Request.spTaskInsert", task, "Handyman_DB").First();
                    return taskId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
