using HandymanProviderLibrary.Models;

namespace HandymanProviderLibrary.Api.Service
{
    public interface IServiceEndpoint
    {
        Task<List<ServiceModel>> GetServices();

        Task UpdateProviderService(ProviderServiceModel providerService);
        Task DeleteProviderService(ProviderServiceModel providerService);
        Task CreateProviderService(ProviderServiceModel providerService);
        Task<List<ProviderServiceModel>> GetProviderServicesByServiceId(int serviceId);
    }
}