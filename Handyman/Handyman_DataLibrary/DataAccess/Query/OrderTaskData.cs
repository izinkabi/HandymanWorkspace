using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Internal.DataAccess;
using Handyman_DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                 taskId = _dataAccess.SaveData<TaskModel>("Request.spTaskInsert", task, "Handyman_DB");
                    return taskId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
