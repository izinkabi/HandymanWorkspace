using HandymanProviderLibrary.Api.EndPoints.Interface;
using HandymanProviderLibrary.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace Handyman_SP_UI.Pages.Helpers;

public class ProviderHelper : EmployeeHelper, IProviderHelper
{
    IServiceProviderEndPoint? _providerEndPoint;
    AuthenticationStateProvider? _authenticationStateProvider;
    ServiceProviderModel? providerModel;


    public ProviderHelper(IServiceProviderEndPoint providerEndPoint, AuthenticationStateProvider authenticationStateProvider) : base(authenticationStateProvider)
    {
        _providerEndPoint = providerEndPoint;
        _authenticationStateProvider = authenticationStateProvider;

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

            if (providerModel == null && userId != null)
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

            newBiz.date = DateTime.Now;

        newBiz.Employee.employeeId = await GetUserId();
        newBiz.Employee.pro_providerId = await GetUserId();
        newBiz.registration.businessType = newBiz.Type;
        newBiz.Employee.employeeProfile.UserId = newBiz.Employee.employeeId;


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
