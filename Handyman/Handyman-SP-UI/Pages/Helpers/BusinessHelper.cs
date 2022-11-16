using HandymanProviderLibrary.Api.EndPoints.Interface;
using HandymanProviderLibrary.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace Handyman_SP_UI.Pages.Helpers
{
    public class BusinessHelper : IBusinessHelper
    {
        static IBusinessEndPoint _business;
        static AuthenticationStateProvider? _authenticationStateProvider;
        static string? userId;

        public BusinessHelper(IBusinessEndPoint business, AuthenticationStateProvider? authenticationStateProvider)
        {
            _business = business;
            _authenticationStateProvider = authenticationStateProvider;
        }

        //Get the logged in User Id
        async Task<string> GetUserId()
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

        //Get the business with the logged in user
        public async Task<BusinessModel> GetBusinessLoggedInEmployee()
        {
            try
            {
                if (userId == null)
                {
                    userId = await GetUserId();
                }

                var business = await _business.GetLoggedInEmployee(userId);
                return business;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
