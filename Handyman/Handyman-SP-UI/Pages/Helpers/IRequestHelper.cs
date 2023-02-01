using HandymanProviderLibrary.Models;

namespace Handyman_SP_UI.Pages.Helpers
{
    public interface IRequestHelper
    {
        Task<IList<OrderModel>> GetNewRequests(int serviceId);
        Task<OrderModel> GetNewRequest(int serviceId, int orderId);
        Task AcceptRequest(OrderModel newRequest);
        Task<IList<RequestModel>> GetOwnRequests();
        Task<RequestModel> GetOwnRequest(int id);
        Task<TaskModel> GetTask(int id);
        Task UpdateTask(TaskModel requestUpdate);
        int GetProgress(RequestModel request);
        int CheckStatus(RequestModel request);
    }
}