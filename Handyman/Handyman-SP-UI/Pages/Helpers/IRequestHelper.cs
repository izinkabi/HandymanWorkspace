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
    }
}