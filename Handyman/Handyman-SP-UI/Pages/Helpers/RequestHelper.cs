using HandymanProviderLibrary.Api.EndPoints.Interface;
using HandymanProviderLibrary.Models;

namespace Handyman_SP_UI.Pages.Helpers;
/// <summary>
/// This class is responsible for the requests of a service provider
/// </summary>
public class RequestHelper : IRequestHelper
{
    IRequestEndPoint? _requestEndPoint;
    IProviderHelper _providerHelper;
    IList<OrderModel> orders;
    IList<RequestModel> _requests;
    public RequestHelper(IRequestEndPoint requestEndPoint, IProviderHelper providerHelper)
    {
        _requestEndPoint = requestEndPoint;
        _providerHelper = providerHelper;
    }

    List<RequestModel> NewRequests = new()!;
    List<RequestModel> StartedRequests = new()!;
    List<RequestModel> AcceptedRequests = new()!;
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
            return orders;
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
        newRequest.service = new()!;
        try
        {
            if (orders == null)
            {
                orders = await GetNewRequests(serviceId);
            }

            foreach (var nr in orders)
            {
                if (nr.Id == orderId)
                {
                    newRequest = nr;
                }
            }
            return newRequest;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
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
        nr.req_status = "1";
        nr.req_progress = 1;

        try
        {
            //save the accepted request
            await _requestEndPoint.PostRequest(await _providerHelper.StampNewRequest(nr));

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
    /// <summary>
    /// This method categorises a requests by progess
    /// </summary>
    /// <param name="progress"> This is the progress parameter</param>
    /// <returns></returns>
    public List<RequestModel>? GetRequestsInCategory(int progress)
    {
        try
        {


            //List<RequestModel> NewRequests = new()!;
            //List<RequestModel> StartedRequests = new()!;
            //List<RequestModel> AcceptedRequests = new()!;
            //List<RequestModel> FinishedRequests = new()!;

            if (_requests.Count > 0)
            {
                foreach (var request in _requests)
                {
                    if (request.req_progress == progress && progress == 1)
                    {
                        //Accepted
                        StartedRequests.Add(request);
                    }

                    if (request.req_progress == progress && progress == 0)
                    {
                        //New
                        NewRequests.Add(request);
                    }
                    if (request.req_progress == progress && progress == 2)
                    {
                        //Started
                        StartedRequests.Add(request);

                    }
                    if (request.req_progress == progress && progress == 3)
                    {
                        //Finished
                        FinishedRequests.Add(request);
                    }
                }

                switch (progress)
                {
                    case 0: return NewRequests;
                    case 1:
                        {
                            return AcceptedRequests;
                            break;
                        }
                    case 2:
                        {
                            return StartedRequests;
                            break;
                        }
                    case 3:
                        {
                            return FinishedRequests;
                            break;
                        }

                }


            }
            return null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

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


}
