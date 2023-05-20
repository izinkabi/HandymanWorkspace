using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Handyman_Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Member,Owner")]
public class RequestsController : ControllerBase
{
    IRequestData _requestData;


    public RequestsController(IRequestData requestData)
    {
        _requestData = requestData;
        //_orderData = orderData;
    }


    // GET: api/<RequestsController>/5
    [HttpGet]
    [Route("GetRequest")]
    public RequestModel GetRequest(int Id)
    {
        try
        {
            RequestModel newRequest = _requestData.GetRequest(Id);
            return newRequest;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    //--------Filtering Methods
    [HttpGet]
    [Route("GetCurrentWeek")]
    public IList<RequestModel> GetCurrentWeekRequests(string empID) => _requestData.GetCurrentWeekRequests(empID);
    [HttpGet]
    [Route("GetCurrentMonth")]
    public IList<RequestModel> GetCurrentMonthRequests(string empID) => _requestData.GetCurrentMonthRequests(empID);
    [HttpGet]
    [Route("GetCancelled")]
    public IList<RequestModel> GetCancelledRequests(string empID) => _requestData.GetCancelledRequests(empID);
    //----------End Filtering methods

    // GET: api/<RequestsController>/5
    [HttpGet]
    [Route("GetNewRequests")]
    public IList<OrderModel> GetNewRequests(int serviceId)
    {
        try
        {
            if (serviceId > 0)
            {
                IList<OrderModel> newRequests = _requestData.GetNewRequests(serviceId);
                return newRequests;
            }

            return null;
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
            if (Id > 0)
            {
                TaskModel request = _requestData.GetTask(Id);
                return request;
            }
            return null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    // POST api/<RequestsController>
    [HttpPost]
    [Route("Post")]
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
    [HttpPut]
    [Route("Update")]
    public void Update(TaskModel taskUpdate)
    {
        try
        {
            _requestData.UpdateTask(taskUpdate);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

}
