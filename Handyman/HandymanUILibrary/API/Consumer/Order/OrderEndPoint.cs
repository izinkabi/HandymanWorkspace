using HandymanUILibrary.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace HandymanUILibrary.API.Consumer.Order
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
        public async Task<OrderModel> PostOrder(OrderModel order, List<TaskModel> todoList)
        {
            var httpResponseMessage = await _aPIHelper.ApiClient.PostAsJsonAsync("/api/SaveOrder", new
            {
                order.ConsumerID,
                order.ServiceId,
                order.Stage,
                order.IsAccepted,
                TodoItems = todoList

            });


            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var result = await httpResponseMessage.Content.ReadFromJsonAsync<OrderModel>();
                return result;
            }
            else
            {
                throw new Exception(httpResponseMessage.ReasonPhrase);
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
            try
            {
                List<OrderModel> httpResponseMessage = await _aPIHelper.ApiClient.GetFromJsonAsync<List<OrderModel>>($"/api/GetOrdersByConsumerId?Id={customerId}");


                return httpResponseMessage;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
            try
            {
                HttpResponseMessage httpResponseMessage = await _aPIHelper.ApiClient.DeleteAsync($"/api/DeleteOrder?Id={Id}");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var result = await httpResponseMessage.Content.ReadFromJsonAsync<OrderModel>();
                }
                else
                {
                    throw new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
