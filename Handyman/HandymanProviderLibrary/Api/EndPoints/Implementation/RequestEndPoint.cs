using HandymanProviderLibrary.Api.ApiHelper;
using HandymanProviderLibrary.Api.EndPoints.Interface;
using HandymanProviderLibrary.Models;
using System.Net.Http.Json;


namespace HandymanProviderLibrary.Api.EndPoints.Implementation;

public class RequestEndPoint : IRequestEndPoint
{

    private IAPIHelper? _apiHelper;
    public RequestEndPoint(IAPIHelper? aPIHelper)
    {
        _apiHelper = aPIHelper;
    }

    //Get the orders relavant to the provider
    public async Task<List<OrderModel>> GetAllOrdersByService(int serviceId)
    {
        List<OrderModel>? httpResponseMessage = new();

        try
        {
            httpResponseMessage = await _apiHelper.ApiClient.GetFromJsonAsync<List<OrderModel>>($"/api/GetAllOrdersByService?serviceId={serviceId}");
            return httpResponseMessage;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public async Task<List<RequestModel>> GetRequestsByProvider(string? providerId)
    {
        List<RequestModel>? httpResponseMessage = new();

        try
        {
            httpResponseMessage = await _apiHelper.ApiClient.GetFromJsonAsync<List<RequestModel>>($"/api/GetRequestsByProviderId?providerId={providerId}");
            return httpResponseMessage;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public async Task<string> PostRequest(RequestModel request)
    {
        string? result = string.Empty;
        var req = new
        {
            request.ServiceId,
            request.ProviderId,
            request.OrderId,
            DateCreated = request.DateAccepted,

        };

        try
        {
            var httpResponseMessage = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Request/Post", req);
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


    public async Task<string> UpdateRequest(RequestModel updateRequest)
    {
        string? result;
        var req = new
        {
            updateRequest.Id,
            updateRequest.ProviderId,
            updateRequest.Status
        };

        try
        {
            var httpResponseMessage = await _apiHelper.ApiClient.PutAsJsonAsync("/api/Request/Update", req);
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

}

