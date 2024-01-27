using HandymanUILibrary.API.Consumer.Order.Interface;
using HandymanUILibrary.API.User;
using HandymanUILibrary.Models;
using HandymanUILibrary.Models.Auth;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace HandymanUILibrary.API.Consumer.Order.Implementation;

public class OrderEndPoint : IOrderEndPoint
{
    private readonly IAPIHelper _apiHelper;
    private readonly AuthenticatedUserModel _authenticatedUser;

    /// <summary>
    /// This method is used to construct a the API helper
    /// </summary>
    /// <param name="aPIHelper"></param>

    public OrderEndPoint(IAPIHelper aPIHelper, AuthenticatedUserModel authenticatedUser)
    {
        _apiHelper = aPIHelper;
        _authenticatedUser = authenticatedUser;

    }

    /// <summary>
    /// This method is used to Post a request to the API
    /// </summary>
    /// <param name="order"></param>
    /// <returns>OrderModel</returns>
    /// <exception cref="Exception">Empty Model Exception</exception>
    public async Task<int> PostOrder(OrderModel order)
    {
        HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
        try
        {
            if (_authenticatedUser.Access_Token != null)
            {
                _apiHelper.ApiClient.DefaultRequestHeaders.Clear();
                _apiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
                _apiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/*+json"));
                _apiHelper.ApiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_authenticatedUser.Access_Token}");
                httpResponseMessage = await _apiHelper.ApiClient.PostAsJsonAsync<OrderModel>("/api/requests/Post", order);
            }

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
    public async Task<IList<OrderModel>> GetOrders()
    {
        try
        {
            IList<OrderModel> orders = new List<OrderModel>();
            if (_authenticatedUser.Access_Token != null)
            {
                _apiHelper.ApiClient.DefaultRequestHeaders.Clear();
                _apiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
                _apiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/*+json"));
                _apiHelper.ApiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_authenticatedUser.Access_Token}");
                orders = await _apiHelper.ApiClient.GetFromJsonAsync<IList<OrderModel>>($"/api/requests/GetRequests");
            }

            return orders;


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
