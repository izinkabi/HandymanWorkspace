using HandymanProviderLibrary.Api.ApiHelper;
using HandymanProviderLibrary.Api.EndPoints.Interface;
using HandymanProviderLibrary.Models;
using System.Net.Http.Json;


namespace HandymanProviderLibrary.Api.EndPoints.Implementation;

public class RequestEndPoint : IRequestEndPoint
{

    IAPIHelper? _apiHelper;
    public RequestEndPoint(IAPIHelper? aPIHelper)
    {
        _apiHelper = aPIHelper;
    }

    //Get the orders relavant to the provider
    //This allows an order filter by the provider's service(s)
    public async Task<IList<OrderModel>> GetNewRequestsByService(int serviceId)
    {
        List<OrderModel>? httpResponse = new();

        try
        {
            httpResponse = await _apiHelper.ApiClient.GetFromJsonAsync<List<OrderModel>>($"/api/Requests/GetNewRequests?serviceId={serviceId}");
            return httpResponse;
        }
        catch (Exception ex)
        {
            return null;
            throw new Exception(ex.Message, ex.InnerException);
        }

    }

    //Get the provider's request
    //Using the ID of the service provider prompt for request
    public async Task<List<RequestModel>> GetRequestsByProvider(string? providerId)
    {
        List<RequestModel>? httpResponseMessage = new();

        try
        {
            httpResponseMessage = await _apiHelper.ApiClient.GetFromJsonAsync<List<RequestModel>>($"/api/Requests/GetProviderRequests?providerId={providerId}");
            return httpResponseMessage;
        }
        catch (Exception ex)
        {
            return null;
            throw new Exception(ex.Message, ex.InnerException);
        }

    }

    //Post a request
    //This method is invoked when the provider accepts
    //an order that was place by a consumer
    public async Task<string> PostRequest(RequestModel request)
    {
        string? result = string.Empty;

        try
        {
            var httpResponseMessage = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Requests", request);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                result = httpResponseMessage.ReasonPhrase;
                return result;
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
            return ex.Message;
        }
        return null;
    }

    //Update a request
    //This method is called when there is a change in the request by the provider
    //Change of state/stage, cancellation of completion by the service provider
    public async Task<string> UpdateRequest(RequestModel updateRequest)
    {
        string? result;
        //var req = new
        //{
        //    updateRequest.req_id,
        //    updateRequest.req_orderid,
        //    updateRequest.req_status,
        //    updateRequest.tasks
        //};

        try
        {
            var httpResponseMessage = await _apiHelper.ApiClient.PutAsJsonAsync<RequestModel>("/api/Requests/Update", updateRequest);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                result = httpResponseMessage.ReasonPhrase;
                return result;
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return null;
    }

    //Get a task by its ID
    public async Task<TaskModel> GetTask(int id)
    {

        try
        {
            TaskModel taskmodel = await _apiHelper.ApiClient.GetFromJsonAsync<TaskModel>($"/api/Requests/GetTask?Id={id}");
            return taskmodel;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }



}

