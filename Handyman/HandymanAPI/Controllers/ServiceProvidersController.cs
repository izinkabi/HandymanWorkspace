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

        // GET: api/ServiceProviders/5
        [Route("api/GetServiceProviderById")]
        public ServiceProviderModel GetServiceProviderById(string userId)
        {
            providerData = new ServiceProviderData();
            return providerData.GetProviderById(userId);
        }

        // POST: api/ServiceProviders
        public void PostProvider(ServiceProviderModel serviceProviderModel)
        {
            providerData = new ServiceProviderData();
            providerData.PostProvider(serviceProviderModel);
        }

        //GET: api/GetProvidersServices
        public List<ProvidersServiceModel> GetProvidersServices()
        {
            providerData = new ServiceProviderData();
            var list = providerData.GetProvidersServices();
            return list;
        }

        //Get: api/GetProvidersService/Id
        //public ProvidersServiceModel GetProvidersService(int Id)
        //{
        //    providerData = new ServiceProviderData();
        //    return providerData.GetProvidersServicesById(Id);
        //}
        // PUT: api/ServiceProviders/5
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
