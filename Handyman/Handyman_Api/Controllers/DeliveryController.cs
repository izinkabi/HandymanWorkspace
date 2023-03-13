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
    [Route("GetBusiness")]
    public BusinessModel? Get(int? busId)
    {

        try
        {
            if (busId != null && busId.Value != 0)
            {
                business = new()!;
                business = _businessData.GetBusinessById(busId.Value);
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
    [Route("Create")]
    public BusinessModel CreateBusiness(BusinessModel business)
    {
        try
        {
            if (business != null)
            {
                BusinessModel businessM = _businessData.CreateBusiness(business);
                if (businessM != null) return businessM;

            }

            return null;

        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    //Update the business or its address
    [HttpPut]
    [Route("Update")]
    public void Update()
    {

    }

    //Employ a new member
    [HttpPost]
    [Route("AddNewMember")]
    public void AddNewMember(ServiceProviderModel serviceProvider)
    {
        try
        {
            if (serviceProvider != null && providermodel.pro_providerId != null)
                _businessData.EmployServiceProvider(serviceProvider);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpGet]
    [Route("GetProvider")]
    public ServiceProviderModel GetProvider(string employeeId)
    {

        try
        {
            if (employeeId != null)
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
    [Route("PostProviderService")]
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
