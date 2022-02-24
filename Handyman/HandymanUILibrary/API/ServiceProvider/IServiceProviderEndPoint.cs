using HandymanUILibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HandymanUILibrary.API
{
    public interface IServiceProviderEndPoint
    {

        Task<ServiceProviderModel> PostServiceProvider(ServiceProviderModel serviceProviderModel);
        Task<ServiceProviderModel> GetProviderByProfileId(int profileId);
        Task<List<ServiceProviderModel>> GetServiceProviders();
        Task<List<ProvidersServiceModel>> GetProvidersServiceByProviderId(int providerId);//a list of the services provided by the one handyman
        Task<ProvidersServiceModel> PostProvidersService(ProvidersServiceModel providersService);
        Task DeleteProvidersService(int Id);
        Task<ServiceProviderModel> UpdateServiceProvider(ServiceProviderModel serviceProvider);

    }
}