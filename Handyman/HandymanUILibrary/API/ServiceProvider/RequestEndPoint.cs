using HandymanUILibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace HandymanUILibrary.API.ServiceProvider
{
    public class RequestEndPoint : IRequestEndPoint
    {
        private IAPIHelper _aPIHelper;

        /// <summary>
        /// This method is used to construct a the API helper
        /// </summary>
        /// <param name="aPIHelper"></param>
        public RequestEndPoint(IAPIHelper aPIHelper)
        {
            _aPIHelper = aPIHelper;
        }

        /// <summary>
        /// This method is used to Post a request to the API
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<RequestModel> PostRequest(RequestModel request)
        {


            HttpResponseMessage responseMessage = await _aPIHelper.ApiClient.PostAsJsonAsync("/api/PostRequest", request);

            if (responseMessage.IsSuccessStatusCode)
            {
                var result = await responseMessage.Content.ReadFromJsonAsync<RequestModel>();
                return result;
            }
            else
            {
                throw new Exception(responseMessage.ReasonPhrase);
            }
        }
        /// <summary>
        /// This method is used to Find a request using the Customer ID from the API
        /// </summary>
        /// <param name="providerId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<RequestModel> GetRequest(string providerId)
        {


            using (HttpResponseMessage httpResponseMessage = await _aPIHelper.ApiClient.GetAsync($"/api/GetRequestsByProviderId?providerId={providerId}"))
            {
                if (httpResponseMessage.IsSuccessStatusCode)
                {

                    var result = await httpResponseMessage.Content.ReadFromJsonAsync<RequestModel>();
                    return result;
                }
                else
                {
                    throw new Exception(httpResponseMessage.ReasonPhrase);
                }
            }

        }
        /// <summary>
        /// This method is used to update a request from the API
        /// </summary>
        /// <param name="requestUpdate"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task UpdateRequest(RequestModel requestUpdate)
        {
            HttpResponseMessage responseMessage = await _aPIHelper.ApiClient.PostAsJsonAsync("api/UpdateProfile", requestUpdate);

            if (responseMessage.IsSuccessStatusCode)
            {
                var result = await responseMessage.Content.ReadFromJsonAsync<RequestModel>();

            }
            else
            {
                throw new Exception(responseMessage.ReasonPhrase);
            }
        }
        /// <summary>
        /// This methos is used to Delete a request from the API
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteRequest(int Id)
        {
            using (HttpResponseMessage httpResponseMessage = await _aPIHelper.ApiClient.DeleteAsync($"/api/Request/Delete?Id={Id}"))//Deleting the request of the parameterized request id 
            {
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var result = await httpResponseMessage.Content.ReadFromJsonAsync<RequestModel>();
                }
                else
                {
                    throw new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
        }
    }

}
