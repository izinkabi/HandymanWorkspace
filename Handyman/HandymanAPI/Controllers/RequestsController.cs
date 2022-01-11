using HandymanDataLibray.DataAccess;
using HandymanDataLibray.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace HandymanAPI.Controllers
{
    public class RequestsController : ApiController
    {
        private RequestData requestData;


        //GET 
        //api/Request/@ConsumerId
        [Route("api/GetRequestsByConsumerId")]
        public List<RequestModel> GetRequestsByConsumerId(int customerId)
        {          
            requestData = new RequestData();
            return requestData.GetRequestsByConsumerId(customerId);
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
