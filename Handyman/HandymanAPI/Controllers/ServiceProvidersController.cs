using HandymanDataLibrary.Models;
using HandymanDataLibray.DataAccess;
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
        public IEnumerable<ServiceProviderModel> Get()
        {
            providerData = new ServiceProviderData();
            return providerData.GetServiceProviders();
        }

        // GET: api/ServiceProviders/5
        public ServiceProviderModel Get(int id)
        {
            providerData = new ServiceProviderData();
            return providerData.GetProviderById(id);
        }

        // POST: api/ServiceProviders
        public void Post(ServiceProviderModel serviceProviderModel)
        {
            providerData = new ServiceProviderData();
            providerData.PostProvider(serviceProviderModel);
        }

        // PUT: api/ServiceProviders/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ServiceProviders/5
        public void Delete(int id)
        {
        }
    }
}
