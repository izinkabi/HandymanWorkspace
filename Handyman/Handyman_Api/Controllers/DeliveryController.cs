using Handyman_DataLibrary.DataAccess.Interfaces;
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

    /// <summary>
    /// Get the business under which the employee(userId) which is a provider is registered
    /// </summary>
    /// <param name="busId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
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

    /// <summary>
    /// Register A business
    /// </summary>
    /// <param name="business"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
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



    /// <summary>
    /// Employ a new member
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <exception cref="Exception"></exception>
    [HttpPost]
    [Route("AddNewMember")]
    public void AddNewMember(ServiceProviderModel serviceProvider)
    {
        try
        {
            if (serviceProvider != null && serviceProvider.pro_providerId != null)
            {
                if (string.IsNullOrEmpty(serviceProvider.employeeId))
                {
                    serviceProvider.employeeId = serviceProvider.pro_providerId;
                }

                if (!string.IsNullOrEmpty(serviceProvider.employeeId))
                {
                    _businessData.EmployServiceProvider(serviceProvider);
                }
                else
                {
                    return;
                }
            }

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

    /// <summary>
    /// Post a Member/Service Provider
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <exception cref="Exception"></exception>
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


    /// <summary>
    /// Get a workshop by Registration ID
    /// </summary>
    /// <param name="regNumber">registration ID</param>
    /// <returns>WorkShopMpdel</returns>
    /// <exception cref="Exception"></exception>
    [HttpGet]
    [Route("GetWorkShop")]
    public BusinessModel Get(string regNumber)
    {
        try
        {
            var workshop = _businessData.GetWorkShop(regNumber);
            return workshop;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    //Insert new workshop service
    [HttpPost]
    [Route("InsertWorkShopService")]
    public void Post(int workshopRegId, int customServiceId)
    {
        if (workshopRegId == 0 || workshopRegId == 0)
        {
            return;
        }

        try
        {
            _businessData.InsertWorkShopService(workshopRegId, customServiceId);
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
}
