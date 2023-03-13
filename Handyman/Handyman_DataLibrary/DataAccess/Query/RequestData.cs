using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Internal.DataAccess;
using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Query;

public class RequestData : IRequestData
{
    ISQLDataAccess _dataAccess;
    ITaskData _taskData;
    IServiceData _serviceData;

    public RequestData(ISQLDataAccess dataAccess, ITaskData taskData, IServiceData serviceData)
    {
        _dataAccess = dataAccess;
        _taskData = taskData;
        _serviceData = serviceData;

    }

    //Get all the requests for the given service and their tasks
    //These are actually orders that represent new requests
    public IList<OrderModel> GetNewRequests(int serviceId)
    {
        List<OrderTaskModel> ordertasks = new()!;
        HashSet<OrderModel> orderSet = new()!;//It does not allow duplicates
        try
        {
            ordertasks = _dataAccess.LoadData<OrderTaskModel, dynamic>("Delivery.spOrderLookUpByService",
                     new { serviceId = serviceId }, "Handyman_DB");

            //Braking down the ordertask entity
            //First get get orders then get services
            foreach (var ordertask in ordertasks)
            {
                var order = new OrderModel();
                order.service = new();
                //populate order

                order.Id = ordertask.ord_id;
                order.duedate = ordertask.ord_duedate;
                order.status = ordertask.ord_status;


                //populate service of each order
                order.service = _serviceData.GetService(serviceId);

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
    //Service of the given Request


    //Get Task by ID
    public TaskModel GetTask(int Id) => _taskData.GetTask(Id);

    //Get the request(s) of the given provider and their tasks
    public IList<RequestModel> GetRequests(string providerId)
    {
        try
        {
            //Get the requests first
            List<RequestModel> requests = _dataAccess.LoadData<RequestModel, dynamic>("Delivery.spRequestLookUpByProvider", new { providerId = providerId }, "Handyman_DB");


            //Get tasks for each order, hence for each request 
            if (requests != null && requests.Count > 0)
            {
                foreach (RequestModel request in requests)
                {
                    //Get the service
                    request.Service = _serviceData.GetServiceByOrder(request.req_orderid);

                    //Get tasks
                    request.tasks = _taskData.GetTasks(request.req_orderid).ToList();

                }
            }


            return requests;
        }
        catch (Exception ex)
        {
            return null;
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    /// <summary>
    /// Post or insert a new request and it's tasks
    /// </summary>
    /// <param name="request"></param>
    /// <exception cref="Exception"></exception>
    //This can only be made by the provider given
    //the fact that it is an order accepted by the provider
    public void InsertRequest(RequestModel request)
    {
        try
        {
            //insert a request
            _dataAccess.StartTransaction("Handyman_DB");
            _dataAccess.SaveDataTransaction("Delivery.spRequestInsert",
                new
                {
                    req_datecreated = request.req_datecreated,
                    req_employeeid = request.req_employeeid,
                    req_progress = request.req_progress,
                    req_orderid = request.req_orderid,
                    req_status = request.req_status
                });

            //update the accepted order
            _dataAccess.SaveDataTransaction("Delivery.spOrderUpdateByRequest", new { orderId = request.req_orderid, status = 2 });
            _dataAccess.CommitTransation();
        }
        catch (Exception ex)
        {
            _dataAccess.RollBackTransaction();
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    /// <summary>
    /// Update a task for a given request
    /// </summary>
    /// <param name="task"></param>
    public void UpdateRequest(RequestModel requestUpdate)
    {
        try
        {
            if (requestUpdate != null)
            {
                _dataAccess.StartTransaction("Handyman_DB");
                _dataAccess.SaveDataTransaction("Delivery.spRequestUpdate", requestUpdate);

                foreach (var item in requestUpdate.tasks)
                {
                    _dataAccess.SaveDataTransaction("Request.spTaskUpdate", item);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    //Get Request by an ID
    public RequestModel GetRequest(int id)
    {
        try
        {
            RequestModel request = new()!;
            if (id > 0)
            {
                request = _dataAccess.LoadData<RequestModel, dynamic>("Delivery.spRequestLookUpByProvider", new { orderId = id }, "Handyman_DB").FirstOrDefault();
            }


            if (request != null && request.req_orderid != 0)
            {
                //Get a service
                request.Service = _serviceData.GetServiceByOrder(request.req_orderid);
                //Get tasks
                request.tasks = _taskData.GetTasks(request.req_orderid).ToList();

                return request;
            }
            return request;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
            throw;
        }
    }

    //Update a task of a request
    public void UpdateTask(TaskModel taskUpdate) => _taskData.UpdateTask(taskUpdate);

    /// <summary>
    /// Getting Provider's Current month requests
    /// </summary>
    /// <param name="employeeId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public IList<RequestModel> GetCurrentMonthRequests(string employeeId)
    {
        try
        {
            IList<RequestModel> thisMonthRequests = _dataAccess.LoadData<RequestModel, dynamic>("Delivery.spRequestsLookUp_ByCurrentMonth", new { employeeId = employeeId }, "Handyman_DB");

            if (thisMonthRequests != null && thisMonthRequests.Count > 0)
            {
                foreach (RequestModel request in thisMonthRequests)
                {
                    request.tasks = _taskData.GetTasks(request.req_orderid).ToList();
                    request.Service = _serviceData.GetServiceByOrder(request.req_orderid);
                }

            }

            return thisMonthRequests;

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
    public IList<RequestModel> GetCurrentWeekRequests(string employeeId)
    {
        try
        {
            IList<RequestModel>? thisWeekRequests = _dataAccess.LoadData<RequestModel, dynamic>("Delivery.spRequestsLookUp_ByCurrentWeek", new { employeeId = employeeId }, "Handyman_DB");

            if (thisWeekRequests != null && thisWeekRequests.Count > 0)
            {
                foreach (RequestModel request in thisWeekRequests)
                {
                    request.tasks = _taskData.GetTasks(request.req_orderid).ToList();
                    request.Service = _serviceData.GetServiceByOrder(request.req_orderid);
                }

            }

            return thisWeekRequests;

        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message, ex.InnerException);
        }
    }


    /// <summary>
    /// Requests that are flagged as cancelled
    /// </summary>
    /// <param name="employeeId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public IList<RequestModel> GetCancelledRequests(string employeeId)
    {
        try
        {
            IList<RequestModel> cancelledRequests = _dataAccess.LoadData<RequestModel, dynamic>("Delivery.spCancelledRequestLookUp", new { employeeId = employeeId }, "Handyman_DB");

            if (cancelledRequests != null && cancelledRequests.Count > 0)
            {
                foreach (RequestModel request in cancelledRequests)
                {

                    request.tasks = _taskData.GetTasks(request.req_orderid).ToList();
                    request.Service = _serviceData.GetServiceByOrder(request.req_orderid);
                }

            }

            return cancelledRequests;

        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message, ex.InnerException);
            return null;
        }
    }
}

