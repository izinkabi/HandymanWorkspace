using HandymanUILibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HandymanUILibrary.API
{
    public class RequestEndPoint:IRequestEndPoint
    {
        private IAPIHelper _aPIHelper;
       

        public RequestEndPoint(IAPIHelper aPIHelper)
        {
            _aPIHelper = aPIHelper;
        }

        //Post a request
        public async Task<RequestModel> PostRequest(RequestModel request)
        {


            HttpResponseMessage responseMessage = await _aPIHelper.ApiClient.PostAsJsonAsync("/api/PostRequest", request);

            if (responseMessage.IsSuccessStatusCode)
            {
                var result = await responseMessage.Content.ReadAsAsync<RequestModel>();
                return result;
            }
            else
            {
                throw new Exception(responseMessage.ReasonPhrase);
            }
        }

        //Get requests by customerId
        public async Task<RequestModel> GetRequest(int customerId)
        {


            using (HttpResponseMessage httpResponseMessage = await _aPIHelper.ApiClient.GetAsync($"/api/GetRequestsByConsumerId?customerId={customerId}"))
            {
                if (httpResponseMessage.IsSuccessStatusCode)
                {

                    var result = await httpResponseMessage.Content.ReadAsAsync<RequestModel>();
                    return result;
                }
                else
                {
                    throw new Exception(httpResponseMessage.ReasonPhrase);
                }
            }

        }

        //Update request of the passed id
        public async Task UpdateRequest(RequestModel requestUpdate)
        {
            HttpResponseMessage responseMessage = await _aPIHelper.ApiClient.PostAsJsonAsync("api/UpdateProfile", requestUpdate);

            if (responseMessage.IsSuccessStatusCode)
            {
                var result = await responseMessage.Content.ReadAsAsync<RequestModel>();

            }
            else
            {
                throw new Exception(responseMessage.ReasonPhrase);
            }
        }


        //Delete a request of the passed id if it exists
        public async Task DeleteRequest(int Id)
        {
            using (HttpResponseMessage httpResponseMessage = await _aPIHelper.ApiClient.DeleteAsync($"/api/Request/Delete?Id={Id}"))//Deleting the request of the parameterized request id 
            {
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var result = await httpResponseMessage.Content.ReadAsAsync<RequestModel>();
                }
                else
                {
                    throw new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
        }
    }

}
