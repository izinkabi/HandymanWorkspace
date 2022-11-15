using Handyman_DataLibrary;
using Handyman_DataLibrary.DataAccess.Query;
using Handyman_DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace Handyman_Api.Controllers;

[Route("api/Delivery")]
[ApiController]
public class DeliveryController : ControllerBase
{
    IBusinessData _businessData;
    public DeliveryController(IBusinessData business)
    {
        _businessData = business;
    }

    //Get the business under which the employee(userId) which is a provider is registered
    [HttpGet]
    [Route("Get")]
    public BusinessModel Get(string employeeid)
    {
        try
        {
            var business = _businessData.GetBusiness(employeeid);
            return business;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //Register A business
    [HttpPost]
    [Route("Business/Create")]
    public int CreateBusiness(BusinessModel business)
    {
        try
        {
            int businessId = _businessData.CreateBusiness(business);
            return businessId;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //Update the business or its address
    [HttpPut]
    [Route("Business/Update")]
    public void Update()
    {

    }

    //Employ a new member
    [HttpPost]
    [Route("Business/AddNewMember")]
    public void AddNewMember(ServiceProviderModel serviceProvider)
    {
        try
        {
            _businessData.EmployMember(serviceProvider);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }



}
