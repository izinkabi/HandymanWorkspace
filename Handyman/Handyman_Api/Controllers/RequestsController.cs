using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Handyman_Api.Controllers;

[Route("api/Requests")]
[ApiController]
//[Authorize(Roles = "Consumer,Member,Owner")]
public class RequestsController : ControllerBase
{
    IRequestData _requestData;
    private readonly SignInManager<IdentityUser> _signInManager;
    IEnumerable<RequestModel>? Requests;
    public RequestsController(IRequestData requestData, SignInManager<IdentityUser> signInManager)
    {
        _requestData = requestData;
        _signInManager = signInManager;
    }
    // GET: api/<RequestsController>
    [HttpGet]
    [Route("GetRequests")]
    public IEnumerable<RequestModel> Get()
    {
        try
        {

            var user = _signInManager.Context.User;
            var id = "162cdb1e-d4e2-44d4-9589-aa3c8b647f30";//user.FindFirst(u => u.Type.Contains("nameidentifier"))?.Value;
            if (id != null)
                Requests = _requestData.GetRequests(id);
            return Requests;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    // POST api/<RequestsController>
    [HttpPost]
    [Route("Post")]
    public int Post(RequestModel request)
    {
        try
        {
            if (Request == null)
            {
                return 0;
            }
            return _requestData.InsertRequest(request);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    // PUT api/<RequestsController>/5
    [HttpPut]
    [Route("Update")]
    public void Update(RequestModel requestUpdate)
    {
        try
        {
            if (requestUpdate == null)
            {
                return;
            }
            _requestData.UpdateRequest(requestUpdate);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    // DELETE api/<RequestsController>/5
    [HttpDelete]
    public void Delete(string consumerId, int RequestId)
    {
        try
        {
            _requestData.DeleteRequestAndTasks(consumerId, RequestId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }
}
