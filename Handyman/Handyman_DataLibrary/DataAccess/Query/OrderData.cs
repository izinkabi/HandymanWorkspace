using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Internal.DataAccess;
using Handyman_DataLibrary.Models;



namespace Handyman_DataLibrary.DataAccess.Query;

public class OrderData : IOrderData
{
    ISQLDataAccess _dataAccess;
    TaskData taskData;
    public OrderData(ISQLDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
        taskData = new TaskData(dataAccess);
    }

    /// <summary>
    /// Get the consumer's orders and their respective tasks
    /// </summary>
    /// <param name="consumerID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public IEnumerable<OrderModel> GetOrders(string consumerID)
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

                Service_CategoryModel service = _dataAccess.LoadData<Service_CategoryModel, dynamic>("Request.spServiceLookUpBy_Id",
                     new { serviceId = ordertask.ord_service_id }, "Handyman_DB").First();
                //populate service of each order
                order.service.name = service.serv_name;
                order.service.status = service.serv_status;
                order.service.datecreated = service.serv_datecreated;
                order.service.img = service.serv_img;
                order.service.id = service.serv_id;


                order.service.category = new ServiceCategoryModel();

                //populate category
                order.service.category.name = service.cat_name;
                order.service.category.description = service.cat_description;
                order.service.category.type = service.cat_type;

                //Check if the has been populated already
                foreach (var o in orderSet)
                {
                    if (o.Id == order.Id)
                    {
                        orderSet.Remove(o);
                    }
                }
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
                    task.tas_description = ordertask.tas_description;
                    task.tas_date_finished = ordertask.tas_date_finished;
                    task.tas_date_started = ordertask.tas_date_started;
                    task.tas_title = ordertask.tas_title;
                    task.task_id = ordertask.task_id;
                    task.tas_duration = ordertask.tas_duration;
                    task.tas_status = ordertask.tas_status;
                    if (order.Id == ordertask.ord_id)
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


    public void InsertOrder(OrderModel order)
    {
        try
        {
            var DateCreated = DateTime.Now;
            /*Save order*/
            _dataAccess.StartTransaction("Handyman_DB");
            int orderId = _dataAccess.LoadDataTransaction<int, dynamic>("Request.spOrderInsert", new
            {
                ConsumerID = order.ConsumerID,
                DateCreated = order.datecreated,
                Status = order.status,
                DueDate = order.duedate,
                ServiceId = order.service.id
            }).First();

            /*Saving the task*/

            foreach (var item in order.Tasks)
            {
                //Save the task item

                //get a new task id
                int taskId = taskData.InsertTask(item);
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
                    DateCreated = order.datecreated,
                    Status = order.status,
                    DueDate = order.duedate,
                    Id = order.Id

                },
                "Handyman_DB");
            //
            if (order.Tasks.Count() > 0)
            {
                foreach (var task in order.Tasks)
                {
                    if (task != null)
                        taskData.UpdateTask(task);
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
            _dataAccess.StartTransaction("Handyman_DB");
            //Delete the request first
            _dataAccess.SaveDataTransaction("Request.spRequestDelete", new { orderId = orderId });
            //Then delete order and it's tasks
            _dataAccess.SaveDataTransaction("Request.spDeleteOrderTask", new { consumerId = consumerId, OrderId = orderId });
            //Commit the transaction
            _dataAccess.CommitTransation();
        }
        catch (Exception ex)
        {
            _dataAccess.RollBackTransaction();

            throw new Exception(ex.Message);
            _dataAccess.Dispose();
        }
    }

    /// <summary>
    /// Get orders and its
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public IList<OrderModel> GetOrder(int orderId)
    {
        List<OrderTaskModel> ordertasks = new()!;
        HashSet<OrderModel> orderSet = new()!;//It does not allow duplicates
        try
        {
            ordertasks = _dataAccess.LoadData<OrderTaskModel, dynamic>("Delivery.spOrderLookUpByService",
                     new { orderId = orderId }, "Handyman_DB");

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

                Service_CategoryModel service = _dataAccess.LoadData<Service_CategoryModel, dynamic>("Request.spServiceLookUpBy_Id",
                     new { serviceId = ordertask.ord_service_id }, "Handyman_DB").First();
                //populate service of each order
                order.service.name = service.serv_name;
                order.service.status = service.serv_status;
                order.service.datecreated = service.serv_datecreated;
                order.service.img = service.serv_img;
                order.service.id = service.serv_id;


                order.service.category = new ServiceCategoryModel();

                //populate category
                order.service.category.name = service.cat_name;
                order.service.category.description = service.cat_description;
                order.service.category.type = service.cat_type;

                //Check if the has been populated already
                foreach (var o in orderSet)
                {
                    if (o.Id == order.Id)
                    {
                        orderSet.Remove(o);
                    }
                }
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
                    task.tas_description = ordertask.tas_description;
                    task.tas_date_finished = ordertask.tas_date_finished;
                    task.tas_date_started = ordertask.tas_date_started;
                    task.tas_title = ordertask.tas_title;
                    task.task_id = ordertask.task_id;
                    //task.duration = ordertask.tas_duration;
                    if (order.Id == ordertask.ord_id)
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



    /// <summary>
    /// Insert a new task
    /// </summary>
    /// <param name="task"></param>
    /// <returns></returns>
    public int InsertTask(TaskModel task) => taskData.InsertTask(task);

    //Updating a task alone



    /////////////////////////////////////////////////
    /// / <summary>
    /// /An internal class for task
    /// </summary>    ///
    /////////////////////////////////////////////////


    internal class TaskData
    {
        static ISQLDataAccess? _dataAccess;

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
                task = _dataAccess.LoadData<TaskModel, dynamic>("Request.spTaskLookUp", new { taskId = id }, "Handyman_DB").First();
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
        internal void UpdateTask(TaskModel taskUpdate)
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
    }
}
