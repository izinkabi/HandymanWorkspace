using SP_MLibrary.Models;

namespace SP_MLibrary.Services.Interface
{
    public interface IServiceEndpoint
    {
        Task<List<ServiceModel>> GetServices();

        Task UpdateProviderService(MemberServiceModel providerService);
        Task DeleteProviderService(MemberServiceModel providerService);
        Task<string> CreateProviderService(MemberServiceModel providerService);
        Task<List<MemberServiceModel>> GetProviderServicesByServiceId(int serviceId);
        Task<List<MemberServiceModel>> GetProviderServiceByProviderId(string providerId);
        Task<PriceModel> GetPrice(int priceId);
        Task<int> CreateCustomService(ServiceModel service);
        Task<List<CustomServiceModel>> GetWorkShopServices(int wsregId);
        Task<bool> UpdateWorkShopService(CustomServiceModel workshopService);
        Task<bool> DeleteWorkShopService(int wsServiceId, int wsRegId);
        Task<bool> DeleteMemberService(int wsServiceId, string providerId);
    }
}