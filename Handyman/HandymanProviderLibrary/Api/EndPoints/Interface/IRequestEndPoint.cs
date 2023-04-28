using HandymanProviderLibrary.Models;

namespace HandymanProviderLibrary.Api.EndPoints.Interface
{
    public interface IRequestEndPoint
    {
        Task<IList<OrderModel>> GetNewRequestsByService(int serviceId);
        Task<List<RequestModel>> GetRequestsByProvider(string? providerId);
        Task<bool> PostRequest(RequestModel request);
        Task<bool> UpdateTask(TaskModel taskUpdate);
        Task<TaskModel> GetTask(int id);
        Task<List<RequestModel>> GetCurrentMonthRequests(string empID);
        Task<List<RequestModel>> GetCurrentWeekRequests(string empID);
        Task<IList<RequestModel>> GetCancelledRequests(string empID);
        Task<PriceModel> GetTaskPrice(int TaskId);
        Task<bool> PostTaskPrice(int taskId, int priceId);
    }
}