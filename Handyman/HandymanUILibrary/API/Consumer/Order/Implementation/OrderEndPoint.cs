using HandymanUILibrary.API.Consumer.Order.Interface;
using HandymanUILibrary.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace HandymanUILibrary.API.Consumer.Order.Implementation;

public class OrderEndPoint : IOrderEndPoint
{
    IAPIHelper _aPIHelper;

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
    /// <returns>OrderModel</returns>
    /// <exception cref="Exception">Empty Model Exception</exception>
    public async Task PostOrder(OrderModel order)
    {
        try
        {
            var httpResponseMessage = await _aPIHelper.ApiClient.PostAsJsonAsync<OrderModel>("/api/orders/Post", /*new
        {
            order.ConsumerID,
            order.service,
            order.datecreated,
            order.duedate,
            Tasks = order.Tasks,
            Id = order.Id,
            status = order.status

        }*/ order);

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
            IList<OrderModel> httpResponseMessage = await _aPIHelper.ApiClient.GetFromJsonAsync<IList<OrderModel>>($"/api/orders/GetOrders?consumerId={customerId}");

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
    /// <returns>Task</returns>
    /// <exception cref="Exception"></exception>
    public async Task UpdateOrder(OrderModel orderUpdate)
    {
        try
        {
            var result = await _aPIHelper.ApiClient.PutAsJsonAsync<OrderModel>("/api/Orders/Put", orderUpdate);

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
    //public async Task DeleteOrder(int Id)
    //{
    //    try
    //    {
    //        HttpResponseMessage httpResponseMessage = await _aPIHelper.ApiClient.DeleteAsync($"/api/Delete?Id={Id}");

    //        if (httpResponseMessage.IsSuccessStatusCode)
    //        {
    //            var result = await httpResponseMessage.Content.ReadFromJsonAsync<OrderModel>();
    //        }
    //        else
    //        {
    //            throw new Exception(httpResponseMessage.ReasonPhrase);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message);
    //    }
    //}
}
