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
        private static ServiceData serviceData;

        // GET: api/Services
        [Route("api/Services/GetServies")]
        public List<ServiceModel> Get()
        {
             serviceData = new ServiceData();
            return serviceData.GetAllServices();
        }

        // GET: api/ServicesById/5
        [Route("api/GetServiesByCategoryId")]
        public IEnumerable<ServiceModel> Get(int id)
        {
            serviceData = new ServiceData();

            return serviceData.GetServicesByCategoryId(id);
        }
        [Route("api/PostService")]
        // POST: api/Services
        public void Post([FromBody]ServiceModel service)
        {
            serviceData = new ServiceData();
            serviceData.PostService(service);
        }

        // PUT: api/Services/5
        [Route("api/UpdateService")]
        public void Put(int id, [FromBody]ServiceModel service)
        {
          //  serviceData = new ServiceData();
          
        }

        // DELETE: api/Services/5
        public void Delete(int id)
        {
        }
    }
}
