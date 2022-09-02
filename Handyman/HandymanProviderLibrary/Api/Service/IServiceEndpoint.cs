using HandymanProviderLibrary.Models;

namespace HandymanProviderLibrary.Api.Service
{
    public interface IServiceEndpoint
    {
        Task<List<ServiceModel>> GetServices();

        Task UpdateProviderService(ProviderServiceModel providerService);
        Task DeleteProviderService(ProviderServiceModel providerService);
        Task<string> CreateProviderService(ProviderServiceModel providerService);
        Task<List<ProviderServiceModel>> GetProviderServicesByServiceId(int serviceId);
        Task<List<ProviderServiceModel>> GetProviderServiceByProviderId(string providerId);
    }
}