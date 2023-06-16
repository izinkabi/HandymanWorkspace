using HandymanProviderLibrary.Models;

namespace Handyman_SP_UI.Helpers
{
    public interface IRequestHelper
    {
        Task<IList<OrderModel>> GetNewRequests(int serviceId);
        Task<OrderModel> GetNewRequest(int serviceId, int orderId);
        Task<List<RequestModel>> GetCurrentMonthRequests();
        Task<List<RequestModel>> GetCurrentWeekRequests();
        Task<IList<RequestModel>> GetCancelledRequests(string empID);
        Task<bool> AcceptRequest(OrderModel newRequest);
        Task<List<RequestModel>> GetOwnRequests();
        Task<RequestModel> GetOwnRequest(int id);
        Task<TaskModel> GetTask(int id);
        Task UpdateTask(TaskModel requestUpdate);
        int GetProgress(RequestModel request);
        int CheckStatus(RequestModel request);
        Task<RequestModel> ConfirmAccepted(OrderModel request);
        Task<bool> IsAccepted(OrderModel order);
        Task<PriceModel> GetTaskPrice(int TaskId);
    }
}