using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Internal.DataAccess;
using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Query;

public class OrderData : IOrderData
{
    ISQLDataAccess _dataAccess;
    ITaskData _taskData;
    IServiceData _serviceData;

    public OrderData(ISQLDataAccess dataAccess, ITaskData taskData, IServiceData serviceData)
    {
        _dataAccess = dataAccess;
        _taskData = taskData;
        _serviceData = serviceData;

    }

    //Get all the Orders for the given service and their tasks
    //These are actually orders that represent new Orders
    public IList<OrderModel> GetNewOrders(int serviceId)
    {
        List<RequestModel> orders = new()!;
        HashSet<OrderModel> orderSet = new()!;//It does not allow duplicates
        try
        {
            orders = _dataAccess.LoadData<RequestModel, dynamic>("Delivery.spOrderLookUpByService",
                     new { serviceId = serviceId }, "Handyman_DB");

            //Braking down the ordertask entity
            //First get get orders then get services
            foreach (var ord in orders)
            {
                var order = new OrderModel();
                order.tasks = new();
                //populate order

                order.order_id = ord.Id;
                order.order_datecreated = ord.datecreated;
                order.order_status = ord.status;

                //populate service of each order
                order.Service = _serviceData.GetService(serviceId);

                //Check if the has been populated already
                foreach (var r in orderSet)
                {
                    if (r.order_id == order.order_id)
                    {
                        orderSet.Remove(r);
                    }
                }
                orderSet.Add(order);


            }
            //populate task

            foreach (var order in orderSet)
            {
                order.tasks = new List<TaskModel>();
                foreach (var tsk in order.tasks)
                {
                    var task = new TaskModel()!;

                    //populate task
                    task.tas_description = tsk.tas_description;
                    task.tas_date_finished = tsk.tas_date_finished;
                    task.tas_date_started = tsk.tas_date_started;
                    task.tas_title = tsk.tas_title;
                    task.task_id = tsk.task_id;
                    //task.duration = ordertask.tas_duration;
                    if (order.order_id == order.order_id)
                    {
                        order.tasks.Add(task);
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
    //Service of the given Order


    //Get Task by ID
    public TaskModel GetTask(int Id) => _taskData.GetTask(Id);

    //Get the Order(s) of the given provider and their tasks
    public IList<OrderModel> GetOrders(string providerId)
    {
        try
        {
            //Get the Orders first
            List<OrderModel> Orders = _dataAccess.LoadData<OrderModel, dynamic>("Delivery.spOrderLookUpByProvider", new { providerId = providerId }, "Handyman_DB");


            //Get tasks for each order, hence for each Order 
            if (Orders != null && Orders.Count > 0)
            {
                foreach (OrderModel Order in Orders)
                {
                    //Get the service
                    Order.Service = _serviceData.GetServiceByOrder(Order.order_id);

                    //Get tasks
                    Order.tasks = _taskData.GetTasks(Order.order_id).ToList();

                }
            }


            return Orders;
        }
        catch (Exception ex)
        {
            return null;
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    /// <summary>
    /// Post or insert a new Order and it's tasks
    /// </summary>
    /// <param name="Order"></param>
    /// <exception cref="Exception"></exception>
    //This can only be made by the provider given
    //the fact that it is an order accepted by the provider
    public void InsertOrder(OrderModel Order)
    {
        try
        {
            //insert a Order
            _dataAccess.StartTransaction("Handyman_DB");
            _dataAccess.SaveDataTransaction("Delivery.spOrderInsert",
                new
                {
                    order_datecreated = Order.order_datecreated,
                    order_employeeid = Order.order_employeeid,
                    order_progress = Order.order_progress,
                    order_orderid = Order.order_id,
                    order_status = Order.order_status
                });

            //update the accepted order
            _dataAccess.SaveDataTransaction("Delivery.spOrderUpdateByOrder", new { orderId = Order.order_id, status = 2 });
            _dataAccess.CommitTransation();
        }
        catch (Exception ex)
        {
            _dataAccess.RollBackTransaction();
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    /// <summary>
    /// Update a task for a given Order
    /// </summary>
    /// <param name="task"></param>
    public void UpdateOrder(OrderModel OrderUpdate)
    {
        try
        {
            if (OrderUpdate != null)
            {
                _dataAccess.StartTransaction("Handyman_DB");
                _dataAccess.SaveDataTransaction("Delivery.spOrderUpdate", OrderUpdate);

                foreach (var item in OrderUpdate.tasks)
                {
                    _dataAccess.SaveDataTransaction("Order.spTaskUpdate", item);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    //Get Order by an ID
    public OrderModel GetOrder(int id)
    {
        try
        {
            OrderModel Order = new()!;
            if (id > 0)
            {
                Order = _dataAccess.LoadData<OrderModel, dynamic>("Delivery.spOrderLookUpByProvider", new { orderId = id }, "Handyman_DB").FirstOrDefault();
            }


            if (Order != null && Order.order_id != 0)
            {
                //Get a service
                Order.Service = _serviceData.GetServiceByOrder(Order.order_id);
                //Get tasks
                Order.tasks = _taskData.GetTasks(Order.order_id).ToList();

                return Order;
            }
            return Order;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
            throw;
        }
    }

    //Update a task of a Order
    public void UpdateTask(TaskModel taskUpdate) => _taskData.UpdateTask(taskUpdate);

    /// <summary>
    /// Getting Provider's Current month Orders
    /// </summary>
    /// <param name="employeeId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public IList<OrderModel> GetCurrentMonthOrders(string employeeId)
    {
        try
        {
            IList<OrderModel> thisMonthOrders = _dataAccess.LoadData<OrderModel, dynamic>("Delivery.spOrdersLookUp_ByCurrentMonth", new { employeeId = employeeId }, "Handyman_DB");

            if (thisMonthOrders != null && thisMonthOrders.Count > 0)
            {
                foreach (OrderModel Order in thisMonthOrders)
                {
                    Order.tasks = _taskData.GetTasks(Order.order_id).ToList();
                    Order.Service = _serviceData.GetServiceByOrder(Order.order_id);
                }

            }

            return thisMonthOrders;

        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message, ex.InnerException);
        }
    }


    /// <summary>
    /// Similar to the above method only differ in date (week)
    /// </summary>
    /// <param name="employeeId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public IList<OrderModel> GetCurrentWeekOrders(string employeeId)
    {
        try
        {
            IList<OrderModel>? thisWeekOrders = _dataAccess.LoadData<OrderModel, dynamic>("Delivery.spOrdersLookUp_ByCurrentWeek", new { employeeId = employeeId }, "Handyman_DB");

            if (thisWeekOrders != null && thisWeekOrders.Count > 0)
            {
                foreach (OrderModel Order in thisWeekOrders)
                {
                    Order.tasks = _taskData.GetTasks(Order.order_id).ToList();
                    Order.Service = _serviceData.GetServiceByOrder(Order.order_id);
                }

            }

            return thisWeekOrders;

        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message, ex.InnerException);
        }
    }


    /// <summary>
    /// Orders that are flagged as cancelled
    /// </summary>
    /// <param name="employeeId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public IList<OrderModel> GetCancelledOrders(string employeeId)
    {
        try
        {
            IList<OrderModel> cancelledOrders = _dataAccess.LoadData<OrderModel, dynamic>("Delivery.spCancelledOrderLookUp", new { employeeId = employeeId }, "Handyman_DB");

            if (cancelledOrders != null && cancelledOrders.Count > 0)
            {
                foreach (OrderModel Order in cancelledOrders)
                {

                    Order.tasks = _taskData.GetTasks(Order.order_id).ToList();
                    Order.Service = _serviceData.GetServiceByOrder(Order.order_id);
                }

            }

            return cancelledOrders;

        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message, ex.InnerException);
            return null;
        }
    }
}

