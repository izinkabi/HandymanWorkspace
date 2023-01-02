using HandymanProviderLibrary.Api.EndPoints.Interface;
using HandymanProviderLibrary.Models;

namespace Handyman_SP_UI.Pages.Helpers;

public class RequestHelper : IRequestHelper
{
    IRequestEndPoint? _requestEndPoint;
    IBusinessHelper _businessHelper;
    IList<OrderModel> orders;
    public RequestHelper(IRequestEndPoint requestEndPoint, IBusinessHelper businessHelper)
    {
        _requestEndPoint = requestEndPoint;
        _businessHelper = businessHelper;
    }

    //Helper method for getting all the orders of the given service
    //These orders will be turned to requests when accepted by provider(s)
    //For now they are refered to as new request(s)
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

    //Get the new request from the new requests list
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

    //Make / create a new request
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
            await _requestEndPoint.PostRequest(await _businessHelper.StampNewRequest(nr));

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    public async Task<IList<RequestModel>> GetOwnRequests()
    {
        IList<RequestModel> requests;

        try
        {
            ServiceProviderModel provider = await _businessHelper.GetProvider();//not sure if this guy will budge!
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

    //Look for the request of the given ID
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
}
