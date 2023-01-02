using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;


namespace Handyman_Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RequestsController : ControllerBase
{
    IRequestData _requestData;
    //IOrderTaskData _orderData;

    public RequestsController(IRequestData requestData)
    {
        _requestData = requestData;
        //_orderData = orderData;
    }

    // GET: api/<RequestsController>/5
    [HttpGet]
    [Route("GetNewRequests")]
    public IList<OrderModel> GetNewRequests(int serviceId)
    {
        try
        {
            IList<OrderModel> newRequests = _requestData.GetNewRequests(serviceId);
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
    [HttpGet]
    [Route("GetTask")]
    public TaskModel GetTask(int Id)
    {
        try
        {
            TaskModel request = _requestData.GetTask(Id);
            return request;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    // POST api/<RequestsController>
    [HttpPost]
    public void Post(RequestModel requestModel)
    {
        try
        {
            _requestData.InsertRequest(requestModel);
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
