﻿using HandymanDataLibrary.Models;
using HandymanDataLibray.DataAccess;
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
        public IEnumerable<ServiceModel> GetServices()
        {
            serviceData = new ServiceData();
            return serviceData.GetServices();
        }

        // GET: api/Services/5
        public ServiceModel Get(int id)
        {
            serviceData = new ServiceData();
            return serviceData.GetServiceById(id);
        }

        // POST: api/Services
        public void Post(ServiceModel service)
        {
            serviceData = new ServiceData();
            serviceData.PostService(service);
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
