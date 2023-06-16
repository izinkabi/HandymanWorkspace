
using HandymanProviderLibrary.Models;

namespace Handyman_SP_UI.Helpers
{
    public interface IProviderHelper
    {
        Task AddService(ServiceModel provider);
        Task<ProfileModel> GetProviderProfile();
        Task<ServiceProviderModel> GetProvider();
        Task<bool> IsServiceProvided(ServiceModel service);
        void RegisterHandyman(ServiceProviderModel newHandyman);
        Task<bool> RegisterProfile(ProfileModel profile);
        Task<BusinessModel> StampBusinessUserAsync(BusinessModel? newBiz);
        Task<RequestModel> StampNewRequest(RequestModel newRequest);
        Task<PriceModel> GetServicePrice(int priceId);
        Task<bool> InsertCustomService(ServiceModel service);
        Task<List<CustomServiceModel>> GetWorkShopServices();
        Task<bool> UpdateWorkShopService(CustomServiceModel wsServices);
        Task<bool> DeleteWorkShopService(int wsServiceId, int ogServiceId);
    }
}