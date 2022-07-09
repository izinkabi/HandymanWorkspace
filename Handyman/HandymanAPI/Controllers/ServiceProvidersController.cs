using HandymanDataLibray.DataAccess;
using HandymanDataLibray.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace HandymanAPI.Controllers
{
  
    public class ServiceProvidersController : ApiController
    {

       
        private ServiceProviderData providerData;

       // GET: api/ServiceProviders
       //[Route("api/GetServiceProviders")]
       // public List<ServiceProviderModel> Get()
       // {
       //     providerData = new ServiceProviderData();
       //     return providerData.GetServiceProviders();
       // }

        // GET: api/GetServiceProviderById/5
        [Route("api/GetServiceProviderById")]
        public ServiceProviderModel GetServiceProviderById(string Id)
        {
            providerData = new ServiceProviderData();
            return providerData.GetProviderById(Id);
        }

        // POST: api/ServiceProviders
       // [Authorize(Roles = "ServiceProvider")]
        public void PostProvider(ServiceProviderModel serviceProviderModel)
        {
            providerData = new ServiceProviderData();
            providerData.PostProvider(serviceProviderModel);
        }

        //GET: api/GetProvidersServices
        [Route("api/GetProvidersServiceByProviderId")]
        public List<ProviderServiceModel> GetProvidersServicesByProviderId(string providerId)
        {
            providerData = new ServiceProviderData();
            var providerService = providerData.GetProvidersServiceByProviderId(providerId);
            return providerService;
        }

      
        [Route("api/PutProvidersService")]
        public void PutProvidersService(ProviderServiceModel providersServiceModel)
        {
            providerData = new ServiceProviderData();
            providerData.PutProvidersService(providersServiceModel.ServiceProviderId, providersServiceModel.Id);
        }

        [Route("api/UpdateServiceProvider")]
        //Update the Service Provider
        public void UpdateServiceProvider(ServiceProviderModel provider)
        {
            providerData = new ServiceProviderData();
            providerData.UpdateServiceProvider(provider);
        }

        [Route("api/DeleteProvidersService")]
        // DELETE: api/ProvidersService/5
        public void DeleteProvidersService(int id)
        {
            providerData = new ServiceProviderData();
            providerData.DeleteProvidersService(id);
        }
    }
}
