using HandymanProviderLibrary.Models;

namespace Handyman_SP_UI.Pages.Helpers
{
    public interface IProviderHelper
    {
        Task AddService(ServiceModel provider);
        Task<ProfileModel> GetProviderProfile();
        Task<ServiceProviderModel> GetProvider();
        Task<bool> IsServiceProvided(ServiceModel service);
        void RegisterHandyman(ServiceProviderModel newHandyman);
        void RegisterProfile(ProfileModel profile);
        Task<BusinessModel> StampBusinessUserAsync(BusinessModel? newBiz);
        Task<RequestModel> StampNewRequest(RequestModel newRequest);
        Task<PriceModel> GetServicePrice(int priceId);
        Task<bool> InsertCustomService(ServiceModel service);
        Task<List<CustomServiceModel>> GetWorkShopServices();
        Task<bool> UpdateWorkShopService(CustomServiceModel wsServices);
    }
}