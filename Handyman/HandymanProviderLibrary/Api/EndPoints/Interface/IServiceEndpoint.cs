using HandymanProviderLibrary.Models;

namespace HandymanProviderLibrary.Api.EndPoints.Interface
{
    public interface IServiceEndpoint
    {
        Task<List<ServiceModel>> GetServices();

        Task UpdateProviderService(ProviderServiceModel providerService);
        Task DeleteProviderService(ProviderServiceModel providerService);
        Task<string> CreateProviderService(ProviderServiceModel providerService);
        Task<List<ProviderServiceModel>> GetProviderServicesByServiceId(int serviceId);
        Task<List<ProviderServiceModel>> GetProviderServiceByProviderId(string providerId);
        Task<PriceModel> GetPrice(int priceId);
        Task<int> CreateCustomService(ServiceModel service);
        Task<List<CustomServiceModel>> GetWorkShopServices(int wsregId);
        Task<bool> UpdateWorkShopService(CustomServiceModel workshopService);
        Task<bool> DeleteWorkShopService(int wsServiceId, int wsRegId);
        Task<bool> DeleteProviderService(int wsServiceId, string providerId);
    }
}