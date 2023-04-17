using Handyman_SP_UI.Areas.Identity.Data;
using HandymanProviderLibrary.Api.EndPoints.Interface;
using HandymanProviderLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Handyman_SP_UI.Pages.Helpers;

public class ProviderHelper : ProfileHelper, IProviderHelper
{
    IServiceProviderEndPoint? _providerEndPoint;
    AuthenticationStateProvider? _authenticationStateProvider;
    ServiceProviderModel? providerModel;
    private RoleManager<IdentityRole> _roleManager;
    private UserManager<Handyman_SP_UIUser> _userManager;
    private AppUserManager _appUserManager;
    private SignInManager<Handyman_SP_UIUser> signInManager;



    public ProviderHelper(IServiceProviderEndPoint providerEndPoint,
        AuthenticationStateProvider authenticationStateProvider,
        RoleManager<IdentityRole> roleManager, AppUserManager appUserManager, UserManager<Handyman_SP_UIUser> userManager, SignInManager<Handyman_SP_UIUser> signInManager)
        : base(providerEndPoint, authenticationStateProvider, userManager, appUserManager,
            roleManager, signInManager)
    {
        _providerEndPoint = providerEndPoint;
        _authenticationStateProvider = authenticationStateProvider;
        _roleManager = roleManager;
        _userManager = userManager;


    }

    //Create a provider profie
    public Task<bool> RegisterProfile(ProfileModel profile) => this.CreateProfile(profile);

    //Get the profile of the Handyman/Provider 
    public async Task<ProfileModel> GetProviderProfile() => await this.GetProfile();

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

            if (userId != null)
            {
                providerModel = await _providerEndPoint?.GetProvider(userId);
                if (providerModel != null)
                {
                    if (providerModel.employeeProfile.UserId == null)
                    {
                        providerModel.employeeProfile = await GetProfile();
                    }
                }
                return providerModel;
            }


            return null;

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
    [Authorize("ServiceProvider")]
    public async Task<BusinessModel> StampBusinessUserAsync(BusinessModel? newBiz)
    {
        if (newBiz != null && newBiz.Employees.Count > 0)
        {
            foreach (var member in newBiz.Employees)
            {
                try
                {
                    if (member.IsOwner)
                    {
                        member.employeeId = await GetUserId();
                        member.pro_providerId = await GetUserId();

                    }
                    else
                    {
                        return null;
                    }
                    newBiz.registration.businessType = newBiz.Type;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                newBiz.date = DateTime.Now;
                //UserManager UI to return registered users
            }


            return newBiz;
        }

        return newBiz;

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
