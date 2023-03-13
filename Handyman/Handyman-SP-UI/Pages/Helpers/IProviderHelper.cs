using HandymanProviderLibrary.Models;

namespace Handyman_SP_UI.Pages.Helpers
{
    public interface IProviderHelper
    {
        Task AddService(ServiceProviderModel provider);
        Task<ProfileModel> GetProviderProfile();
        Task<ServiceProviderModel> GetProvider();
        Task<bool> IsServiceProvided(ServiceModel service);
        void RegisterHandyman(ServiceProviderModel newHandyman);
        void RegisterProfile(ProfileModel profile);
        Task<BusinessModel> StampBusinessUserAsync(BusinessModel? newBiz);
        Task<RequestModel> StampNewRequest(RequestModel newRequest);

    }
}