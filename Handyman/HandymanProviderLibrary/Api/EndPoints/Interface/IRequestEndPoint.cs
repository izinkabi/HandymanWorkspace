using HandymanProviderLibrary.Models;

namespace HandymanProviderLibrary.Api.EndPoints.Interface
{
    public interface IRequestEndPoint
    {
        Task<IList<OrderModel>> GetNewRequestsByService(int serviceId);
        Task<List<RequestModel>> GetRequestsByProvider(string? providerId);
        Task<string> PostRequest(RequestModel request);
        Task UpdateTask(TaskModel taskUpdate);
        Task<TaskModel> GetTask(int id);
        Task<IList<RequestModel>> GetCurrentMonthRequests(string empID);
        Task<IList<RequestModel>> GetCurrentWeekRequests(string empID);
        Task<IList<RequestModel>> GetCancelledRequests(string empID);
    }
}