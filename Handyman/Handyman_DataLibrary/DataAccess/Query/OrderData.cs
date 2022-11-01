using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Internal.DataAccess;
using Handyman_DataLibrary.Models;



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
        public IEnumerable<OrderModel> GetConsumerOrderAndTasks(string consumerID)
        {
            List<OrderTaskModel> ordertasks = new()!;
            HashSet<OrderModel> orderSet = new()!;//It does not allow duplicates
            try
            {
                ordertasks = _dataAccess.LoadData<OrderTaskModel, dynamic>("Request.spOrderLookUp_ByConsumerId_OrderByDateCreated",
                         new { consumerID = consumerID }, "Handyman_DB");
                
                //Braking down the ordertask entity
                //First get get orders then get 
                foreach (var ordertask in ordertasks)
                {
                    var order = new OrderModel();
                    order.service = new();
                    //populate order

                    order.Id = ordertask.ord_id;
                    order.duedate = ordertask.ord_duedate;
                    order.status = ordertask.ord_status;
                    
                    order.service = _dataAccess.LoadData<Service_CategoryModel, dynamic>("Request.spServiceLookUpBy_Id",
                         new { serviceId = ordertask.ord_service_id }, "Handyman_DB").First(); //we need a service model instead
                    orderSet.Add(order);
                    
                }
                //populate task

                foreach (var order in orderSet)
                {
                    order.Tasks = new List<TaskModel>();
                    foreach (var ordertask in ordertasks)
                    {
                        var task = new TaskModel()!;

                        //populate task
                        task.description = ordertask.tas_description;
                        task.dateFinished = ordertask.tas_date_finished;
                        task.dateStarted = ordertask.tas_date_started;
                        task.title = ordertask.tas_title;
                        task.Id = ordertask.task_id;
                        //task.duration = ordertask.tas_duration;
                        if(order.Id == ordertask.ord_id)
                        {
                            order.Tasks.Add(task);
                        }
                       
                    } 
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return orderSet.ToList();
        }


        public void SaveOrder(OrderModel order)
        {
            try
            {
                var DateCreated = DateTime.Now;
                /*Save order*/
                _dataAccess.StartTransaction("Handyman_DB");
                int orderId = _dataAccess.LoadDataTransaction<int,dynamic>("Request.spOrderInsert", new
                {
                    ConsumerID = order.ConsumerID,
                    DateCreated = order.datecreated,
                    Status = order.status,
                    DueDate = order.duedate,
                    ServiceId = order.service.serv_id
                }).First();
               
                /*Saving the task*/

                foreach (var item in order.Tasks)
                {
                    //Save the task item
                    
                    //get a new task id
                  int taskId =  _taskData.InsertTask(item);
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
       
        //Update order and its tasks
        public void UpdateOrder(OrderModel order)
        {
            try
            {
                _dataAccess.SaveData("Request.spOrderUpdate", 
                    new 
                    {
                        ConsumerID = order.ConsumerID,
                        DateCreated= order.datecreated,
                        Status = order.status,
                        DueDate = order.duedate,
                        Id = order.Id

                    },
                    "Handyman_DB");
                //
                if(order.Tasks.Count() > 0)
                {
                    foreach (var task in order.Tasks)
                    {
                        _taskData.UpdateTask(task);
                    }
                    
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Delete order and related tasks go to _dataaccess for more
        /// </summary>
        /// <param name="consumerId"></param>
        /// <param name="orderId"></param>
        /// <exception cref="Exception"></exception>
        public void DeleteOrderAndTasks(string consumerId, int orderId)
        {
            try
            {
                _dataAccess.SaveData("spDeleteOrderTask", new { consumerId = consumerId, OrderId = orderId }, "Handyman_DB");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
