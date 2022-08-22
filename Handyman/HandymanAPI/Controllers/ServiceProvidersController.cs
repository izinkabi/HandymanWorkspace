using HandymanDataLibray.DataAccess;
using HandymanDataLibray.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace HandymanAPI.Controllers
{
  
    public class ServiceProvidersController : ApiController
    {

       
        private ServiceProviderData providerData;

        //GET: api/GetProvidersServices
        //Getting the provider's service by service id
        [Route("api/GetProvidersServicesByProviderId")]
        public List<ProviderServiceModel> GetProvidersServicesByProviderId(string providerId)
        {
            providerData = new ServiceProviderData();
            var providerService = providerData.GetProvidersServiceByProviderId(providerId);
            return providerService;
        }

        //Get all the providers of this service id
        [Route("api/GetProvidersServicesByServiceId")]
        public List<ProviderServiceModel> GetProvidersServicesByServiceId(int serviceId)
        {
            providerData = new ServiceProviderData();
            var providerService = providerData.GetProvidersServiceByServiceId(serviceId);
            return providerService;
        }
        //Updating the provider's service
        [Route("api/PutProvidersService")]
        public void PutProvidersService(ProviderServiceModel providersServiceModel)
        {
            providerData = new ServiceProviderData();
            providerData.PutProvidersService(providersServiceModel.ServiceProviderId, providersServiceModel.Id);
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
