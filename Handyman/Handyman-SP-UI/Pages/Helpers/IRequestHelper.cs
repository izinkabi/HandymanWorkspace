using HandymanProviderLibrary.Models;

namespace Handyman_SP_UI.Pages.Helpers
{
    public interface IRequestHelper
    {
        Task<IList<OrderModel>> GetNewRequests(int serviceId);
    }
}