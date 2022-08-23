using HandymanProviderLibrary.Models;

namespace HandymanProviderLibrary.Api.ServiceProvider
{
    public interface IServiceProviderEndPoint
    {
        Task<List<ProviderServiceModel>> GetProvidersByServiceId(int serviceId);
    }
}