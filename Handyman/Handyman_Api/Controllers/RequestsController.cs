using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;


namespace Handyman_Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RequestsController : ControllerBase
{
    IRequestData _requestData;
    IOrderTaskData _orderData;

    public RequestsController(IRequestData requestData, IOrderTaskData orderData)
    {
        _requestData = requestData;
        _orderData = orderData;
    }

    // GET: api/<RequestsController>/5
    [HttpGet]
    [Route("GetNewRequests")]
    public IList<OrderModel> GetNewRequests(int serviceId)
    {
        try
        {
            IList<OrderModel> newRequests = _orderData.GetOrderAndTasks(serviceId);
            return newRequests;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    // GET api/<RequestsController>/string
    [HttpGet]
    [Route("GetProviderRequests")]
    public IList<RequestModel> GetProviderRequests(string providerId)
    {
        try
        {
            IList<RequestModel> requests = _requestData.GetRequests(providerId);
            return requests;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    // GET api/<RequestsController>/string
    //[HttpGet]
    //[Route("GetTasks")]
    //public IList<TaskModel> GetTasks(int orderId)
    //{
    //    try
    //    {
    //        IList<TaskModel> requests = _requestData.GetTasks(orderId);
    //        return requests;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message, ex.InnerException);
    //    }
    //}

    // POST api/<RequestsController>
    [HttpPost]
    public void Post(RequestModel request)
    {
        try
        {
            _requestData.InsertRequest(request);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    // PUT api/<RequestsController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
        //Implementation
    }

}
