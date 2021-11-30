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
    public class RequestsController : ApiController
    {
        private RequestData requestData;


        //GET 
        //api/Request/@ConsumerId
        public RequestModel GetRequestByConsumerId(int customerId)
        {          
            requestData = new RequestData();
            return requestData.GetRequestByConsumerId(customerId);
        }

        //POST
        public void PostRequest(RequestModel request)
        {
            requestData = new RequestData();
            requestData.PostRequest(request);
        }
    }
}
