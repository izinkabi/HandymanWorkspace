using HandymanUILibrary.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace HandymanUILibrary.API.Consumer
{
    public class OrderEndPoint : IOrderEndPoint
    {
        private IAPIHelper _aPIHelper;

        /// <summary>
        /// This method is used to construct a the API helper
        /// </summary>
        /// <param name="aPIHelper"></param>
       
        public OrderEndPoint(IAPIHelper aPIHelper)
        {
            _aPIHelper = aPIHelper;
        }

        /// <summary>
        /// This method is used to Post a request to the API
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<OrderModel> PostOrder(OrderModel request)
        {


            HttpResponseMessage responseMessage = await _aPIHelper.ApiClient.PostAsJsonAsync("/api/PostOrder", request);

            if (responseMessage.IsSuccessStatusCode)
            {
                var result = await responseMessage.Content.ReadFromJsonAsync<OrderModel>();
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
        /// <param name="customerId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<OrderModel>> GetOrders(string customerId)
        {


            List<OrderModel> httpResponseMessage = await _aPIHelper.ApiClient.GetFromJsonAsync<List<OrderModel>>($"/api/Orders/GetOrdersByConsumerId?providerId={customerId}");
            /*{
                if (httpResponseMessage.IsSuccessStatusCode)
                {

                    var result = await httpResponseMessage.Content.ReadFromJsonAsync<OrderModel>();
                    return result;
                }
                else
                {
                    throw new Exception(httpResponseMessage.ReasonPhrase);
                }
            }*/
            return httpResponseMessage;

        }
        /// <summary>
        /// This method is used to update a request from the API
        /// </summary>
        /// <param name="orderUpdate"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task UpdateOrder(OrderModel orderUpdate)
        {
            HttpResponseMessage responseMessage = await _aPIHelper.ApiClient.PostAsJsonAsync("api/Orders/UpdateOrder", orderUpdate);

            if (responseMessage.IsSuccessStatusCode)
            {
                var result = await responseMessage.Content.ReadFromJsonAsync<OrderModel>();
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
        public async Task DeleteOrder(int Id)
        {
            using (HttpResponseMessage httpResponseMessage = await _aPIHelper.ApiClient.DeleteAsync($"/api/Orders/Delete?Id={Id}"))//Deleting the order of the parameterized request id 
            {
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var result = await httpResponseMessage.Content.ReadFromJsonAsync<OrderModel>();
                }
                else
                {
                    throw new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
        }
    }
}
