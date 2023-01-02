using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Internal.DataAccess;
using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Query;

public class RequestData : IRequestData
{
    ISQLDataAccess _dataAccess;

    public RequestData(ISQLDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
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

    //Get the tasks of a given order
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

    //Get the request(s) of the given provider and their tasks
    public IList<RequestModel> GetRequests(string providerId)
    {
        try
        {
            //Get the requests first
            List<RequestModel> requests = _dataAccess.LoadData<RequestModel, dynamic>("Delivery.spRequestLookUpByProvider", new { providerId = providerId }, "Handyman_DB");
            //Get tasks for each order, hence for each request 
            if (requests.Count > 0)
                foreach (RequestModel request in requests)
                {
                    request.tasks = GetTasks(request.req_orderid).ToList();
                    //_dataAccess.LoadData<TaskModel, dynamic>("Request.spTaskLookUpByOrder", new { orderId = request.req_orderid }, "Handyman_DB");
                }

            return requests;
        }
        catch (Exception ex)
        {
            return null;
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    //Post or insert a new request and it's tasks
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
}
