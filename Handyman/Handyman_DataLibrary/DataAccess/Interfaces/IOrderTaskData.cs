﻿using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Interfaces
{
    public interface IOrderTaskData
    {
        int InsertTask(TaskModel task);
        void UpdateTask(TaskModel taskUpdate);
        IList<OrderModel> GetOrderAndTasks(int orderId);
    }
}