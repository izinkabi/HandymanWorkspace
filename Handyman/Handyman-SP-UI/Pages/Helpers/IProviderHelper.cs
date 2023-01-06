using HandymanProviderLibrary.Models;

namespace Handyman_SP_UI.Pages.Helpers
{
    public interface IProviderHelper
    {
        Task AddService(ServiceProviderModel provider);
        Task<ServiceProviderModel> GetProvider();
        Task<bool> IsServiceProvided(ServiceModel service);
        Task<BusinessModel> StampBusinessUserAsync(BusinessModel? newBiz);
        Task<RequestModel> StampNewRequest(RequestModel newRequest);
    }
}