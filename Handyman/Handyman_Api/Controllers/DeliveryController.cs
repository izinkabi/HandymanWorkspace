using Handyman_DataLibrary;
using Handyman_DataLibrary.DataAccess.Query;
using Handyman_DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace Handyman_Api.Controllers;

[Route("api/Delivery")]
[ApiController]
public class DeliveryController : ControllerBase
{
    IBusinessData? _businessData;
    public DeliveryController(IBusinessData business)
    {
        _businessData = business;
    }
    ServiceProviderModel? providermodel;
    BusinessModel? business;

    //Get the business under which the employee(userId) which is a provider is registered
    [HttpGet]
    [Route("Get")]
    public BusinessModel? Get(string employeeid)
    {

        try
        {
            if (employeeid != null && business == null)
            {
                business = new()!;
                business = _businessData.GetBusiness(employeeid);
                return business;
            }

            return business;
        }
        catch (Exception ex)
        {
            return null;
            throw new Exception(ex.Message, ex.InnerException);
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
            return 0;
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
            if (serviceProvider != null)
                _businessData.EmployServiceProvider(serviceProvider);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpGet]
    [Route("Business/GetProvider")]
    public ServiceProviderModel GetProvider(string employeeId)
    {

        try
        {
            if (providermodel == null && employeeId != null)
            {
                providermodel = _businessData.GetProvider(employeeId);
                return providermodel;
            }
            return providermodel;
        }
        catch (Exception ex)
        {
            return null;
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    [HttpPost]
    [Route("Business/PostProviderService")]
    public void PostProviderService(ServiceProviderModel serviceProvider)
    {
        try
        {
            if (serviceProvider != null)
                _businessData.AddProviderServices(serviceProvider);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }
}
