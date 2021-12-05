using HandymanDataLibrary.Models;
using HandymanDataLibray.DataAccess;
using HandymanDataLibray.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HandymanAPI.Controllers
{
  
    public class ServiceProvidersController : ApiController
    {

        private ServiceProviderModel providerModel;
        private ServiceProviderData providerData;

       // GET: api/ServiceProviders
       [Route("api/GetServiceProviders")]
        public List<ServiceProviderModel> Get()
        {
            providerData = new ServiceProviderData();
            return providerData.GetServiceProviders();
        }

        // GET: api/GetServiceProviderByProfileId/5
        [Route("api/GetServiceProviderByProfileId")]
        public ServiceProviderModel GetServiceProviderByProfileId(int ProfileId)
        {
            providerData = new ServiceProviderData();
            return providerData.GetProviderByProfileId(ProfileId);
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
        public List<ProvidersServiceModel> GetProvidersServicesByProviderId(int providerId)
        {
            providerData = new ServiceProviderData();
            var providerService = providerData.GetProvidersServiceByProviderId(providerId);
            return providerService;
        }

      
        [Route("api/PutProvidersService")]
        public void PutProvidersService(ProvidersServiceModel providersServiceModel)
        {
            providerData = new ServiceProviderData();
            providerData.PutProvidersService(providersServiceModel);
        }

        // DELETE: api/ServiceProviders/5
        public void Delete(int id)
        {
        }
    }
}
