using HandymanProviderLibrary.Models;

namespace HandymanProviderLibrary.Api.Service
{
    public interface IServiceEndpoint
    {
        Task<List<ServiceModel>> GetServices();
    }
}