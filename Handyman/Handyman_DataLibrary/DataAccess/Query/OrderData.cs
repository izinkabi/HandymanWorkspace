﻿using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Internal.DataAccess;
using Handyman_DataLibrary.Models;



namespace Handyman_DataLibrary.DataAccess.Query;

public class OrderData : IOrderData
{
    ISQLDataAccess _dataAccess;
    ITaskData _taskData;
    public OrderData(ISQLDataAccess dataAccess, ITaskData taskData)
    {
        _dataAccess = dataAccess;
        _taskData = taskData;

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
                order.service.PriceId = service.price_id;


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
                //Find provider's details
                foreach (var o in orderSet)
                {
                    order.Provider = GetHandymenDetails(o.Id);
                }



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
                    task.task_id = ordertask.task_id;
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

    //get the provider's details
    private HandymenDetailsModel GetHandymenDetails(int orderId)
    {
        try
        {
            if (orderId != 0)
            {
                var HDM = _dataAccess.LoadData<HandymenDetailsModel, dynamic>("Request.spGetHandymenDetailsByOrder", new { orderId = orderId }, "Handyman_DB").FirstOrDefault();
                if (HDM != null)
                {
                    return HDM;
                }
            }


            return null;
        }
        catch (Exception)
        {

            throw;
        }
    }


    /// <summary>
    /// Insert an order
    /// </summary>
    /// <param name="order"></param>
    public int InsertOrder(OrderModel order)
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
            if (order.Tasks != null && order.Tasks.Count > 0)
            {
                foreach (var item in order.Tasks)
                {
                    //Save the task item

                    //get a new task id
                    int taskId = _taskData.InsertTask(item);
                    //save the order_task 
                    _dataAccess.SaveDataTransaction("Request.spOrder_Task_Insert",
                        new
                        {
                            taskId = taskId,
                            orderId = orderId
                        });
                }
            }



            _dataAccess.CommitTransation();
            return orderId;
        }
        catch
        {
            _dataAccess.RollBackTransaction();
            throw;
        }
    }

    //Update order and request
    public void UpdateOrder(OrderModel order)
    {
        try
        {
            _dataAccess.SaveData("Request.spOrderUpdate",
                new
                {
                    ConsumerID = order.ConsumerID,
                    Status = order.status,
                    DueDate = order.duedate,
                    Id = order.Id

                },
                "Handyman_DB");
            //Update tasks
            if (order.Tasks.Count() > 0)
            {
                foreach (var task in order.Tasks)
                {
                    if (task != null)
                        _taskData.UpdateTask(task);
                }

            }

            //Update Request
            _dataAccess.SaveData("Request.spRequestUpdate",
                new
                {
                    orderId = order.Id,
                    requestStatus = order.status

                },
                "Handyman_DB");

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
    public int InsertTask(TaskModel task) => _taskData.InsertTask(task);

    //Updating a task alone



    /////////////////////////////////////////////////
    /// / <summary>
    /// / Negotiation data class
    /// </summary>    ///
    /////////////////////////////////////////////////



}
