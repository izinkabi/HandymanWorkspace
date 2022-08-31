using HandymanDataLibray.DataAccess;
using HandymanDataLibray.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace HandymanAPI.Controllers
{
    public class RequestsController : ApiController
    {
        private RequestData requestData;


        //Return the orders the provider is related to by service
        [Route("api/GetAllOrdersByService")]
        public List<OrderModel> GetAllOrdersByService(int serviceId)
        {
            requestData = new RequestData();
            
            return requestData.GetAllOrdersByService(serviceId);
        }


        //GET 
        //api/Request/@ConsumerId
        [Route("api/GetRequestsByProviderId")]
        public List<RequestModel> GetRequestsByProviderId(string providerId)
        {
            requestData = new RequestData();
            if (string.IsNullOrEmpty(providerId))
            {
                return null;    
            }    
            return requestData.GetRequestsByProviderId(providerId);
        }

        //POST
        [Route("api/Request/Post")]
        public void Post(RequestModel request)
        {

            requestData = new RequestData();
            requestData.PostRequest(request);
        }

        //UPDATE
        //api/Requests/Update
        [Route("api/Request/Update")]
        public void Put(RequestModel requestUpdate)
        {
            requestData = new RequestData();
            requestData.UpdateRequest(requestUpdate);
        }


        //DELETE
        //api/Requests/delete/5
        public void Delete(int Id)
        {
            requestData = new RequestData();
            requestData.DeleteRequest(Id);
        }
    }
}
