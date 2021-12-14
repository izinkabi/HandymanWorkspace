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

    
    public class ServicesController : ApiController
    {
        // GET: api/Services

        private ServiceModel serviceModel;
        private ServiceData serviceData;

        //Get: api/Services
        [Route ("GetServices")]
        public List<ServiceModel> GetServices()
        {
            serviceData = new ServiceData();
            return serviceData.GetServices();
        }

        // GET: api/Services/5
        [Route("api/GetServiceById")]
        public ServiceModel GetServiceById(int Id)
        {
            serviceData = new ServiceData();
            return serviceData.GetServiceById(Id);
        }

        // POST: api/Services
        public void Post(ServiceModel service)
        {
            serviceData = new ServiceData();
            serviceData.PostService(service);
        }

        //Get the list of service categories
        [Route("api/GetServiceCategory")]
        public List<ServiceCategoryModel> GetServiceCategories()
        {
            serviceData = new ServiceData();
            return serviceData.GetServiceCategories();
        }
        // PUT: api/Services/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE: api/Services/5
        public void Delete(int id)
        {
        }
    }
}
