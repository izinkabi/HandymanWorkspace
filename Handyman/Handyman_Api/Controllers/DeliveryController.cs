using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Handyman_Api.Controllers;

[Route("api/Delivery")]
[ApiController]
//[Authorize(Roles = "ServiceProvider")]
public class DeliveryController : ControllerBase
{
    IBusinessData? _businessData;
    private readonly SignInManager<IdentityUser> signInManager;
    private readonly RoleManager<IdentityRole> roleManager;

    public DeliveryController(IBusinessData business,
        SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _businessData = business;
        this.signInManager = signInManager;
        this.roleManager = roleManager;
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
    public async Task<BusinessModel> CreateBusiness(BusinessModel business)
    {
        try
        {
            if (business != null)
            {
                var claim = signInManager.Context.User;
                //Get the user of the given claim
                var user = await signInManager.UserManager.GetUserAsync(claim);

                string[] roles = new string[] { "Owner", "Member" };
                //Executing the query
                BusinessModel businessM = _businessData.CreateBusiness(business);
                //Member role assignment
                if (businessM.Id != 0)
                {
                    foreach (string role in roles)
                    {
                        var Exists = await roleManager.RoleExistsAsync(role);
                        if (Exists)
                        {
                            var result = await signInManager.UserManager.AddToRoleAsync(user, role);
                        }
                        else
                        {
                            await roleManager.CreateAsync(new IdentityRole(role));
                            var result = await signInManager.UserManager.AddToRoleAsync(user, role);
                        }

                    }
                }

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
    public async Task<IActionResult> AddNewMember(ServiceProviderModel serviceProvider)
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
                    string role = "Member";
                    //Executing the query
                    BusinessModel businessM = _businessData.CreateBusiness(business);
                    //Member role assignment
                    if (businessM.Id != 0)
                    {

                        var claim = signInManager.Context.User;
                        //Get the user of the given claim
                        var user = await signInManager.UserManager.GetUserAsync(claim);
                        var Exists = await roleManager.RoleExistsAsync(role);
                        if (Exists)
                        {
                            var result = await signInManager.UserManager.AddToRoleAsync(user, role);
                            return Ok();
                        }
                        else
                        {
                            await roleManager.CreateAsync(new IdentityRole(role));
                            var result = await signInManager.UserManager.AddToRoleAsync(user, role);
                            return Ok();
                        }

                    }
                }
                else
                {
                    return BadRequest("Invalid Input");
                }
            }
            return Ok();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    /// <summary>
    ///Get the provider / the Member of the Workshop System
    ///It is an employee and has profile 
    /// </summary>
    /// <param name="employeeId"></param>
    /// <returns>ServiceProviderModel</returns>
    /// <exception cref="Exception"></exception>

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
