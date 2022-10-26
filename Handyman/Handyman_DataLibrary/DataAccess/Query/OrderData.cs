using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Internal.DataAccess;
using Handyman_DataLibrary.Models;
using System;


namespace Handyman_DataLibrary.DataAccess.Query
{
    public class OrderData : IOrderData
    {
        ISQLDataAccess _dataAccess;
        IOrderTaskData _taskData;
        public OrderData(ISQLDataAccess dataAccess, IOrderTaskData orderTaskData)
        {
            _dataAccess = dataAccess;
            _taskData = orderTaskData;
        }

        //Get the consumer's orders and their respective tasks
        public List<TaskModel> GetConsumerOrderAndTasks(string consumerID)
        {
            List<TaskModel> orders = new()!;
            try
            {
                orders = _dataAccess.LoadData<TaskModel, dynamic>("Request.spOrderLookUp_ByConsumerId_OrderByDateCreated",
                         new { consumerID = consumerID }, "HandymanDB");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return orders;
        }


        public void SaveOrder(OrderModel order, List<TaskModel> tasks)
        {
            try
            {
                var DateCreated = DateTime.Now;
                //Save order
                _dataAccess.StartTransaction("Handyman_DB");
                _dataAccess.SaveDataTransaction("Request.spOrderInsert", new
                {
                    ConsumerID = order.ConsumerID,
                    DateCreated = order.ord_datecreated,
                    Status = order.ord_status,
                    DueDate = order.ord_duedate,
                    ServiceId = order.ord_service_id
                    //the ordered service because sql wont take a model inside a model
                });
                //Get Id from the order model

                var orderId = _dataAccess.LoadDataTransaction<int, dynamic>("Request.spNewOrderLookUp", new
                {
                    ConsumerID = order.ConsumerID,
                    DateCreated = DateCreated

                }).FirstOrDefault();

                /***************Saving the task****************/

                foreach (var item in tasks)
                {
                    //Save the task item
                    _taskData.InsertTask(item);
                    //get a new task id
                   int taskId = _taskData.GetNewTask(orderId);
                    //save the order_task 
                    _dataAccess.SaveDataTransaction("Request.spOrder_Task_Insert",
                        new
                        {
                            taskId = taskId,
                            orderId = orderId
                        });
                }
               

                _dataAccess.CommitTransation();
            }
            catch
            {
                _dataAccess.RollBackTransaction();
                throw;
            }
        }
       
        public void UpdateOrder(OrderModel order)
        {
            try
            {
                _dataAccess.SaveData("Request.spOrderUpdate", 
                    new 
                    {
                        ConsumerID = order.ConsumerID,
                        DateCreated = order.ord_datecreated,
                        Status = order.ord_status,
                        DueDate = order.ord_duedate,
                        ServiceId = order.ord_service_id
                    },
                    "HandymanDB");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
