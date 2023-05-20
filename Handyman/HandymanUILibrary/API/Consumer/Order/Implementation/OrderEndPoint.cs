using HandymanUILibrary.API.Consumer.Order.Interface;
using HandymanUILibrary.API.User;
using HandymanUILibrary.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace HandymanUILibrary.API.Consumer.Order.Implementation;

public class OrderEndPoint : IOrderEndPoint
{
    IAPIHelper _apiHelper;

    /// <summary>
    /// This method is used to construct a the API helper
    /// </summary>
    /// <param name="aPIHelper"></param>

    public OrderEndPoint(IAPIHelper aPIHelper)
    {
        _apiHelper = aPIHelper;
    }

    /// <summary>
    /// This method is used to Post a request to the API
    /// </summary>
    /// <param name="order"></param>
    /// <returns>OrderModel</returns>
    /// <exception cref="Exception">Empty Model Exception</exception>
    public async Task<int> PostOrder(OrderModel order)
    {
        try
        {
            var httpResponseMessage = await _apiHelper.ApiClient.PostAsJsonAsync<OrderModel>("/orders/Post", order);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                int orderId = await httpResponseMessage.Content.ReadFromJsonAsync<int>();
                if (orderId > 0)
                {
                    return orderId;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
    /// <summary>
    /// This method is used to Find a request using the Customer ID from the API
    /// </summary>
    /// <param name="customerId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<IList<OrderModel>> GetOrders(string customerId)
    {
        try
        {
            if (customerId != null)
            {
                IList<OrderModel> httpResponseMessage = await _apiHelper.ApiClient.GetFromJsonAsync<IList<OrderModel>>($"/api/orders/GetOrders?consumerId={customerId}");
                return httpResponseMessage;
            }

            return null;

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
    /// <returns>Task</returns>
    /// <exception cref="Exception"></exception>
    public async Task UpdateOrder(OrderModel orderUpdate)
    {
        try
        {
            var result = await _apiHelper.ApiClient.PutAsJsonAsync<OrderModel>("/api/Orders/Update", orderUpdate);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
    /// <summary>
    /// This methos is used to Delete a request from the API
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task DeleteOrder(OrderModel order)
    {
        try
        {
            HttpResponseMessage httpResponseMessage = await _apiHelper.ApiClient.DeleteAsync($"/api/Orders?consumerId={order.ConsumerID}&orderId={order.Id}");

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
