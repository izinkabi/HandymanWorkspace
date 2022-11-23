using Handyman_DataLibrary;
using Handyman_DataLibrary.DataAccess.Query;
using Handyman_DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace Handyman_Api.Controllers;

[Route("api/Delivery")]
[ApiController]
public class DeliveryController : ControllerBase
{
<<<<<<< HEAD
    [Route("api/Delivery")]
    [ApiController]
    public class DeliveryController : ControllerBase
=======
    IBusinessData _businessData;
    public DeliveryController(IBusinessData business)
>>>>>>> 1576c75f23d5518700009eba9f6c9919e7494c91
    {
        _businessData = business;
    }

    //Get the business under which the employee(userId) which is a provider is registered
    [HttpGet]
    [Route("Get")]
    public BusinessModel? Get(string employeeid)
    {
        var business = new BusinessModel();
        try
        {
            business = _businessData.GetBusiness(employeeid);
            return business;
        }
<<<<<<< HEAD

        //Get the business under which the employee(userId) is registered
        [HttpGet]
        [Route("GetEmployee")]
        public EmployeeModel GetEmployee(string employeeid)
=======
        catch (Exception ex)
>>>>>>> 1576c75f23d5518700009eba9f6c9919e7494c91
        {
            return null;
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
            _businessData.EmployServiceProvider(serviceProvider);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }



}
