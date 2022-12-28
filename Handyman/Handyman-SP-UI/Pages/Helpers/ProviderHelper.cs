using HandymanProviderLibrary.Api.EndPoints.Interface;
using HandymanProviderLibrary.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace Handyman_SP_UI.Pages.Helpers
{
    public class ProviderHelper : IProviderHelper
    {
        static IServiceProviderEndPoint? _providerEndPoint;
        static AuthenticationStateProvider? _authenticationStateProvider;
        ServiceProviderModel? providerModel;
        EmployeeModel? employeeModel;
        string? ErrorMsg;
        string? userId;
        public ProviderHelper(IServiceProviderEndPoint providerEndPoint, AuthenticationStateProvider authenticationStateProvider)
        {
            _providerEndPoint = providerEndPoint;
            _authenticationStateProvider = authenticationStateProvider;

        }

        //Add new service--this should be in business or business show encapsulate a provider to protect the user ID
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
                ErrorMsg = ex.Message;
            }
        }

        //Get the loggedIn employee if they have an ID
        public async Task<ServiceProviderModel>? GetProvider(string userId)
        {
            if ((userId != null) && (employeeModel == null))
            {
                try
                {
                    providerModel = await _providerEndPoint.GetProvider(userId);

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }

            }
            return providerModel;
        }


        async Task<string>? GetUserId()
        {
            try
            {
                if (userId == null)
                {
                    var user = (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;
                    userId = user.FindFirst(u => u.Type.Contains("nameidentifier"))?.Value;

                }
                return userId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
