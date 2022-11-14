using HandymanProviderLibrary.Models;

namespace HandymanProviderLibrary.Api.EndPoints.Interface
{
    public interface IRequestEndPoint
    {
        Task<List<OrderModel>> GetAllOrdersByService(int serviceId);
        Task<List<RequestModel>> GetRequestsByProvider(string? providerId);
        Task<string> PostRequest(RequestModel request);
        Task<string> UpdateRequest(RequestModel updateRequest);
    }
}