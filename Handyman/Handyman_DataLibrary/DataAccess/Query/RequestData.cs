using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Internal.DataAccess;
using Handyman_DataLibrary.Models;



namespace Handyman_DataLibrary.DataAccess.Query;

public class RequestData : IRequestData
{
    ISQLDataAccess _dataAccess;
    ITaskData _taskData;
    public RequestData(ISQLDataAccess dataAccess, ITaskData taskData)
    {
        _dataAccess = dataAccess;
        _taskData = taskData;

    }

    /// <summary>
    /// Get the consumer's Requests and their respective tasks
    /// </summary>
    /// <param name="consumerID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public IEnumerable<RequestModel> GetRequests(string consumerID)
    {
        List<RequestTaskModel> requestTasks = new()!;
        HashSet<RequestModel> requestSet = new()!;//It does not allow duplicates
        try
        {
            requestTasks = _dataAccess.LoadData<RequestTaskModel, dynamic>("Request.spRequestLookUp_ByConsumerId_OrderByDateCreated",
                     new { consumerID = consumerID }, "Handyman_DB");

            //Braking down the requestTask entity
            //First get get Requests then get 
            foreach (var requestTask in requestTasks)
            {
                var req = new RequestModel();
                req.Service = new();
                //populate req

                req.Id = requestTask.req_id;
                req.duedate = requestTask.req_duedate;
                req.status = requestTask.req_status;

                Service_CategoryModel service = _dataAccess.LoadData<Service_CategoryModel, dynamic>("Request.spServiceLookUpBy_Id",
                     new { serviceId = requestTask.req_service_id }, "Handyman_DB").First();
                //populate Service of each req
                req.Service.name = service.serv_name;
                req.Service.status = service.serv_status;
                req.Service.datecreated = service.serv_datecreated;
                req.Service.img = service.serv_img;
                req.Service.id = service.serv_id;
                req.Service.PriceId = service.price_id;


                req.Service.category = new ServiceCategoryModel();

                //populate category
                req.Service.category.name = service.cat_name;
                req.Service.category.description = service.cat_description;
                req.Service.category.type = service.cat_type;

                //Check if the has been populated already
                foreach (var o in requestSet)
                {
                    if (o.Id == req.Id)
                    {
                        requestSet.Remove(o);
                    }
                }
                requestSet.Add(req);
                //Find provider's details
                foreach (var o in requestSet)
                {
                    req.Member = GetHandymenDetails(o.Id);
                }



            }
            //populate task

            foreach (var request in requestSet)
            {
                request.Tasks = new List<TaskModel>();
                foreach (var requestTask in requestTasks)
                {
                    var task = new TaskModel()!;

                    //populate task
                    task.tas_description = requestTask.tas_description;
                    task.tas_date_finished = requestTask.tas_date_finished;
                    task.tas_date_started = requestTask.tas_date_started;
                    task.tas_title = requestTask.tas_title;
                    task.task_id = requestTask.task_id;
                    task.tas_duration = requestTask.tas_duration;
                    task.tas_status = requestTask.tas_status;
                    task.task_id = requestTask.task_id;
                    if (request.Id == requestTask.req_id)
                    {
                        request.Tasks.Add(task);
                    }

                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return requestSet.ToList();
    }

    //get the provider's details
    private HandymenDetailsModel GetHandymenDetails(int RequestId)
    {
        try
        {
            if (RequestId != 0)
            {
                var HDM = _dataAccess.LoadData<HandymenDetailsModel, dynamic>("Request.spGetHandymenDetailsByRequest", new { RequestId = RequestId }, "Handyman_DB").FirstOrDefault();
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
    /// Insert an req
    /// </summary>
    /// <param name="Request"></param>
    public int InsertRequest(RequestModel Request)
    {
        try
        {
            var DateCreated = DateTime.Now;
            /*Save req*/
            _dataAccess.StartTransaction("Handyman_DB");
            int RequestId = _dataAccess.LoadDataTransaction<int, dynamic>("Request.spRequestInsert", new
            {
                ConsumerID = Request.ConsumerID,
                DateCreated = Request.datecreated,
                Status = Request.status,
                DueDate = Request.duedate,
                ServiceId = Request.Service.id
            }).First();

            /*Saving the task*/
            if (Request.Tasks != null && Request.Tasks.Count > 0)
            {
                foreach (var item in Request.Tasks)
                {
                    //Save the task item

                    //get a new task id
                    int taskId = _taskData.InsertTask(item);
                    //save the Request_task 
                    _dataAccess.SaveDataTransaction("Request.spRequest_Task_Insert",
                        new
                        {
                            taskId = taskId,
                            RequestId = RequestId
                        });
                }
            }



            _dataAccess.CommitTransation();
            return RequestId;
        }
        catch
        {
            _dataAccess.RollBackTransaction();
            throw;
        }
    }

    //Update req and request
    public void UpdateRequest(RequestModel Request)
    {
        try
        {
            _dataAccess.SaveData("Request.spRequestUpdate",
                new
                {
                    ConsumerID = Request.ConsumerID,
                    Status = Request.status,
                    DueDate = Request.duedate,
                    Id = Request.Id

                },
                "Handyman_DB");
            //Update tasks
            if (Request.Tasks.Count() > 0)
            {
                foreach (var task in Request.Tasks)
                {
                    if (task != null)
                        _taskData.UpdateTask(task);
                }

            }

            //Update req
            _dataAccess.SaveData("Request.spRequestUpdate",
                new
                {
                    RequestId = Request.Id,
                    requestStatus = Request.status

                },
                "Handyman_DB");

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    /// <summary>
    /// Delete req and related tasks go to _dataaccess for more
    /// </summary>
    /// <param name="consumerId"></param>
    /// <param name="RequestId"></param>
    /// <exception cref="Exception"></exception>
    public void DeleteRequestAndTasks(string consumerId, int RequestId)
    {
        try
        {
            _dataAccess.StartTransaction("Handyman_DB");
            //Delete the request first
            _dataAccess.SaveDataTransaction("Request.spRequestDelete", new { RequestId = RequestId });
            //Then delete req and it's tasks
            _dataAccess.SaveDataTransaction("Request.spDeleteRequestTask", new { consumerId = consumerId, RequestId = RequestId });
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
    /// Get Requests and its
    /// </summary>
    /// <param name="RequestId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public IList<RequestModel> GetRequest(int RequestId)
    {
        List<RequestTaskModel> requestTasks = new()!;
        HashSet<RequestModel> requestSet = new()!;//It does not allow duplicates
        try
        {
            requestTasks = _dataAccess.LoadData<RequestTaskModel, dynamic>("Delivery.spRequestLookUpByService",
                     new { RequestId = RequestId }, "Handyman_DB");

            //Braking down the requestTask entity
            //First get get Requests then get 
            foreach (var requestTask in requestTasks)
            {
                var req = new RequestModel();
                req.Service = new();
                //populate req

                req.Id = requestTask.task_id;
                req.duedate = requestTask.req_duedate;
                req.status = requestTask.req_status;

                Service_CategoryModel service = _dataAccess.LoadData<Service_CategoryModel, dynamic>("Request.spServiceLookUpBy_Id",
                     new { serviceId = requestTask.req_service_id }, "Handyman_DB").First();
                //populate Service of each req
                req.Service.name = service.serv_name;
                req.Service.status = service.serv_status;
                req.Service.datecreated = service.serv_datecreated;
                req.Service.img = service.serv_img;
                req.Service.id = service.serv_id;


                req.Service.category = new ServiceCategoryModel();

                //populate category
                req.Service.category.name = service.cat_name;
                req.Service.category.description = service.cat_description;
                req.Service.category.type = service.cat_type;

                //Check if the has been populated already
                foreach (var o in requestSet)
                {
                    if (o.Id == req.Id)
                    {
                        requestSet.Remove(o);
                    }
                }
                requestSet.Add(req);


            }
            //populate task
            foreach (var request in requestSet)
            {
                request.Tasks = new List<TaskModel>();
                foreach (var requestTask in requestTasks)
                {
                    var task = new TaskModel()!;

                    //populate task
                    task.tas_description = requestTask.tas_description;
                    task.tas_date_finished = requestTask.tas_date_finished;
                    task.tas_date_started = requestTask.tas_date_started;
                    task.tas_title = requestTask.tas_title;
                    task.task_id = requestTask.task_id;
                    //task.duration = requestTask.tas_duration;
                    if (request.Id == requestTask.req_id)
                    {
                        request.Tasks.Add(task);
                    }

                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return requestSet.ToList();
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
