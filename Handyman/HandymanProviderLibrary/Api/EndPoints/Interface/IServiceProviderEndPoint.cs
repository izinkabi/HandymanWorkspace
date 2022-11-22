using HandymanProviderLibrary.Models;

namespace HandymanProviderLibrary.Api.EndPoints.Interface
{
    public interface IServiceProviderEndPoint
    {
        Task AddService(ServiceProviderModel provider);
        Task RemoveService(ServiceModel service, string providerId);
        Task CreateServiceProvider(ServiceProviderModel provider);
    }
}
