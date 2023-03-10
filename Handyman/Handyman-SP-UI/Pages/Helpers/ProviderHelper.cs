using Handyman_SP_UI.Areas.Identity.Data;
using HandymanProviderLibrary.Api.EndPoints.Interface;
using HandymanProviderLibrary.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Handyman_SP_UI.Pages.Helpers;

public class ProviderHelper : EmployeeHelper, IProviderHelper
{
    IServiceProviderEndPoint? _providerEndPoint;
    AuthenticationStateProvider? _authenticationStateProvider;
    ServiceProviderModel? providerModel;
    RoleManager<IdentityRole> _roleManager;
    UserManager<Handyman_SP_UIUser> _userManager;


    public ProviderHelper(IServiceProviderEndPoint providerEndPoint,
        AuthenticationStateProvider authenticationStateProvider,
        RoleManager<IdentityRole> roleManager, UserManager<Handyman_SP_UIUser> userManager) : base(authenticationStateProvider)
    {
        _providerEndPoint = providerEndPoint;
        _authenticationStateProvider = authenticationStateProvider;
        _roleManager = roleManager;
        _userManager = userManager;

    }


    public async Task<ProfileModel> GetProfile()
    {
        try
        {
            ProfileModel profile = await _providerEndPoint.GetProfile(await GetUserId());
            return profile;
        }
        catch (Exception)
        {

            throw;
        }
    }

    //Register Profile
    public void RegisterProfile(ProfileModel newProfile)
    {
        if (newProfile != null)
        {
            try
            {
                if (userId == null)
                {
                    GetUserId();
                }
                newProfile.UserId = userId;
                ClaimsPrincipal User = GetUser();
                newProfile.EmailAddress = _userManager.GetUserName(User);
                _providerEndPoint.CreateProfile(newProfile);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    //Register a Handyman / Service Provider
    public async void RegisterHandyman(ServiceProviderModel newHandyman)
    {
        if (newHandyman != null && newHandyman.employeeProfile != null)
        {
            GetUserId();
            if (userId != null)
            {
                newHandyman.pro_providerId = userId;
                newHandyman.employeeProfile.UserId = userId;
                await _providerEndPoint.CreateServiceProvider(newHandyman);
            }


        }
    }

    /// <summary>
    /// Add new service using the employee's ID hence the provider's ID
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    public async Task AddService(ServiceProviderModel provider)
    {
        try
        {
            if (provider.pro_providerId == null)
            {
                await GetUserId();
                provider.pro_providerId = userId;
            }

            await _providerEndPoint.AddService(provider);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }
    /// <summary>
    /// Check if the service is being provided
    /// </summary>
    /// <param name="service"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<bool> IsServiceProvided(ServiceModel service)
    {
        try
        {
            if (providerModel == null)
            {
                providerModel = await GetProvider();
            }

            if (providerModel != null && providerModel.Services.Count > 0)
            {
                foreach (var serviceModel in providerModel.Services)
                {
                    if (serviceModel.id == service.id)
                    {
                        return true;
                    }

                }

            }

            return false;
        }
        catch (Exception ex)
        {
            return false;
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    /// <summary>
    /// Get the loggedIn employee if they have an ID
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<ServiceProviderModel> GetProvider()
    {

        try
        {
            if (userId == null)
            {
                userId = await GetUserId();
            }

            if (userId != null && providerModel == null)
            {
                providerModel = await _providerEndPoint?.GetProvider(userId);
            }
            return providerModel;
        }
        catch (Exception ex)
        {
            return null;
            throw new Exception(ex.Message, ex);
        }

    }
    /// <summary>
    /// Stamp the new business with the owner's details / IDs
    /// </summary>
    /// <param name="newBiz"></param>
    /// <returns></returns>
    public async Task<BusinessModel> StampBusinessUserAsync(BusinessModel? newBiz)
    {
        if (newBiz != null)
        {
            foreach (var member in newBiz.Employees)
            {
                newBiz.date = DateTime.Now;
                if (member.IsOwner)
                {
                    //Create a new role in AspNetRoles(owner) and add the users
                    var User = GetUser();
                    if (!User.IsInRole("Owner"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Owner"));
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(await _userManager.GetUserAsync(User), "Owner");
                    }
                    member.employeeId = await GetUserId();
                    member.pro_providerId = await GetUserId();
                }
                else
                {
                    member.employeeId = await GetUserId();
                    member.pro_providerId = await GetUserId();
                }

                newBiz.registration.businessType = newBiz.Type;
                //UserManager UI to return registered users
            }


            return newBiz;
        }

        return null;

    }

    /// <summary>
    /// Get the employee ID hence the provider ID and stamping the request
    /// </summary>
    /// <param name="newRequest"></param>
    /// <returns></returns>
    public async Task<RequestModel> StampNewRequest(RequestModel newRequest)
    {
        if (userId == null)
        {
            userId = await GetUserId();
        }
        if (newRequest != null && userId != null)
        {
            newRequest.req_employeeid = userId;
        }
        return newRequest;

    }
}
