using HandymanUILibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HandymanUILibrary.API
{
    public interface IServiceProviderEndPoint
    {

        Task<ServiceProviderModel> PostServiceProvider(ServiceProviderModel serviceProviderModel);
        Task<ServiceProviderModel> GetProviderById(int Id);
        Task<List<ServiceProviderModel>> GetServiceProviders();
        Task<List<ProviderServiceModel>> GetProvidersServiceByProviderId(int providerId);//a list of the services provided by the one handyman
        Task<ProviderServiceModel> PostProvidersService(ProviderServiceModel providersService);
        Task DeleteProvidersService(int Id);
        Task<ServiceProviderModel> UpdateServiceProvider(ServiceProviderModel serviceProvider);

    }
}