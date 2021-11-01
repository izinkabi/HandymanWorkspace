using HandymanUILibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HandymanUILibrary.API
{
    public interface IRegisterProviderEndPoint
    {

        Task<ServiceProviderModel> PostServiceProvider(ServiceProviderModel serviceProviderModel);
        Task<ServiceProviderModel> GetProviderByUserId(ServiceProviderModel sp);
        Task<ServiceProviderModel> GetServiceProviders(ServiceProviderModel serviceProvider);
        Task<List<ProvidersServiceModel>> GetProvidersServices();
        Task<ProvidersServiceModel> PostProvidersService(ProvidersServiceModel providersService);
    }
}