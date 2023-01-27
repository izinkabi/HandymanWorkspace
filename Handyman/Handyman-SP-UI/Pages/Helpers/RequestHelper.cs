using HandymanProviderLibrary.Api.EndPoints.Interface;
using HandymanProviderLibrary.Models;

namespace Handyman_SP_UI.Pages.Helpers;
/// <summary>
/// This class is responsible for the requests of a service provider
/// </summary>
public class RequestHelper : IDisposable, IRequestHelper
{
    IRequestEndPoint? _requestEndPoint;
    IProviderHelper _providerHelper;
    IList<OrderModel> orders;
    IList<RequestModel> _requests;
    StatusCheckHelper statusCheckHelper;
    RequestModel newRequest;

    public RequestHelper(IRequestEndPoint requestEndPoint, IProviderHelper providerHelper)
    {
        _requestEndPoint = requestEndPoint;
        _providerHelper = providerHelper;
        orders = new List<OrderModel>();
        statusCheckHelper = new StatusCheckHelper();
    }

    List<RequestModel> NewRequests = new()!;
    List<RequestModel> StartedRequests = new()!;
    List<RequestModel> AcceptedRequests;
    List<RequestModel> FinishedRequests = new()!;

    enum RequestStage
    {
        None = 0,
        Accepted = 1,
        Started = 2
    }
    /// <summary>
    /// Helper method for getting all the orders of the given service
    /// These orders will be turned to requests when accepted by provider(s)
    /// For now they are refered to as new request(s)
    /// </summary>
    /// <param name="serviceId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<IList<OrderModel>> GetNewRequests(int serviceId)
    {

        try
        {
            orders = await _requestEndPoint?.GetNewRequestsByService(serviceId);
            if (orders != null)
            {
                return orders;
            }
            return null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    /// <summary>
    /// Get the new request from the new requests list
    /// </summary>
    /// <param name="serviceId"></param>
    /// <param name="orderId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<OrderModel> GetNewRequest(int serviceId, int orderId)
    {
        OrderModel newRequest = new()!;

        try
        {
            if (orders.Count < 1)
            {
                orders = await GetNewRequests(serviceId);
            }

            if (orders != null && orders.Count > 0)
            {
                foreach (var o in orders)
                {
                    if (o.Id == orderId)
                    {
                        newRequest = o;
                        return newRequest;
                    }

                }
            }
            return newRequest;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    //Confirm if the request is accepted
    public async Task<RequestModel> ConfirmAccepted(OrderModel requestChecked)
    {
        if (requestChecked != null)
        {
            if (await IsAccepted(requestChecked))
            {
                return newRequest;
            }
        }
        return null;
    }


    /// <summary>
    /// Check if the order has been accepted
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    public async Task<bool> IsAccepted(OrderModel order)
    {
        try
        {
            if (AcceptedRequests == null || AcceptedRequests.Count == 0)
            {
                AcceptedRequests = (List<RequestModel>?)await GetOwnRequests();
            }

            if (AcceptedRequests != null && AcceptedRequests.Count > 0)
            {
                foreach (var item in AcceptedRequests)
                {
                    if (item.req_orderid == order.Id)
                    {
                        newRequest = item;
                        return true;
                    }
                }
            }
            return false;
        }
        catch (Exception)
        {

            throw;
        }
    }
    /// <summary>
    /// Make / create a new request
    /// </summary>
    /// <param name="newRequest"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>

    public async Task AcceptRequest(OrderModel newRequest)
    {
        RequestModel nr = new()!;
        //populate a request without a provider ID
        nr.req_orderid = newRequest.Id;
        nr.req_datecreated = DateTime.Now;
        nr.req_status = 1;
        nr.req_progress = 1;

        try
        {
            if (!await IsAccepted(newRequest))
            {
                //save the accepted request
                await _requestEndPoint.PostRequest(await _providerHelper.StampNewRequest(nr));

            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }
    /// <summary>
    /// Get the provider's requests
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<IList<RequestModel>> GetOwnRequests()
    {
        IList<RequestModel> requests;

        try
        {
            ServiceProviderModel provider = await _providerHelper.GetProvider();//not sure if this guy will budge!
            if (provider != null)
            {
                requests = await _requestEndPoint.GetRequestsByProvider(provider.pro_providerId);
                return requests;
            }
            return null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }



    //------------------------Updating a request--------------------------//
    public int GetProgress(RequestModel request) => statusCheckHelper.calculateProgress(request);
    public int CheckStatus(RequestModel request) => statusCheckHelper.CheckStatus(request);

    //------------------------End Updating a request--------------------------//



    /// <summary>
    /// Look for the request of the given ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<RequestModel> GetOwnRequest(int id)
    {
        RequestModel request = new()!;
        try
        {
            foreach (var r in await GetOwnRequests())
            {
                if (r.req_id == id)
                {
                    request = r;
                }
            }
            return request;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }
    /// <summary>
    /// Get the task of the given ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<TaskModel> GetTask(int id)
    {
        TaskModel taskModel = new()!;
        try
        {
            taskModel = await _requestEndPoint.GetTask(id);
            return taskModel;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    /// <summary>
    /// Update a given Request (Update the tasks in the request)
    /// </summary>
    /// <param name="requestUpdate"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task UpdateTask(TaskModel taskUpdate)
    {
        try
        {
            if (taskUpdate != null)
            {
                await _requestEndPoint.UpdateTask(taskUpdate);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    void IDisposable.Dispose()
    {
        statusCheckHelper = null;
    }


    ///////////////////////////////////////////////Status Class//////////////////
    /// <summary>
    /// //Status class
    /// </summary>
    internal class StatusCheckHelper
    {

        List<TaskModel>? startedTasks;
        List<TaskModel>? inprogressTasks;
        List<TaskModel>? finishedTasks;



        //Return the request stage
        internal int CheckStatus(RequestModel request)
        {

            if (request != null)
            {
                //initialize attributes
                startedTasks = new()!;
                inprogressTasks = new()!;
                finishedTasks = new()!;

                //Check for task status
                foreach (var t in request.tasks)
                {
                    //STATUS STARTED
                    if (t.tas_status == 1)
                    {
                        startedTasks.Add(t);
                    }
                    //STATUS IN PROGRESS
                    if (t.tas_status == 2)
                    {
                        inprogressTasks.Add(t);
                    }
                    //STATUS CLOSED
                    if (t.tas_status == 3)
                    {
                        finishedTasks.Add(t);
                    }
                    //STATUS CANCELLED
                }

                //Request not started
                if (startedTasks.Count == 0 && inprogressTasks.Count == 0 && finishedTasks.Count == 0)
                {
                    return 1;
                }
                else if (startedTasks.Count == 0 && inprogressTasks.Count == 0 && finishedTasks.Count > 0)//Request closed
                {
                    return 3;
                }
                else //Request inprogress
                {
                    return 2;
                }
            }
            return 0;//Error

        }

        /// <summary>
        /// a psudo algorithm for getting progress using number of tasks per list
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        internal int calculateProgress(RequestModel request)
        {

            if (request != null && request.tasks.Count > 0)
            {
                if (startedTasks.Count == request.tasks.Count)
                {
                    return 10;
                }
                if (startedTasks.Count + inprogressTasks.Count == request.tasks.Count && (startedTasks.Count > 0 && inprogressTasks.Count > 0))
                {
                    return 30;
                }
                if (startedTasks.Count + finishedTasks.Count == request.tasks.Count && (startedTasks.Count > finishedTasks.Count && finishedTasks.Count > 0))
                {
                    return 40;
                }
                if ((inprogressTasks.Count + finishedTasks.Count == request.tasks.Count && inprogressTasks.Count != 0) || inprogressTasks.Count == request.tasks.Count && finishedTasks.Count != request.tasks.Count)
                {
                    return 50;
                }
                if (finishedTasks.Count == request.tasks.Count)
                {
                    return 100;
                }
            }
            return 0;
        }



    }




}

