using HandymanProviderLibrary.Api.EndPoints.Interface;
using HandymanProviderLibrary.Models;

namespace Handyman_SP_UI.Pages.Helpers;

public class RequestHelper : IRequestHelper
{
    IRequestEndPoint? _requestEndPoint;

    public RequestHelper(IRequestEndPoint requestEndPoint)
    {
        _requestEndPoint = requestEndPoint;
    }

    //Helper method for getting all the orders of the given service
    //These orders will be turned to requests when accepted by provider(s)
    //For now they are refered to as new request(s)
    public async Task<IList<OrderModel>> GetNewRequests(int serviceId)
    {
        IList<OrderModel> orders;
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
}
