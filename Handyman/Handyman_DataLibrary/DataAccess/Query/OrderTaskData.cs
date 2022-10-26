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

        public int GetNewTask(int orderId)
        {
            int id = 0;
            try
            {
                id = _dataAccess.LoadDataTransaction<int, dynamic>("Request.spNewTaskLookUp", new
                {
                    OrderId = orderId,

                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return id;
        }

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

        public void InsertTask(TaskModel task)
        {
            try
            {
                var result = _dataAccess.SaveData<TaskModel>("Request.spTaskInsert", task, "Handyman_DB");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
