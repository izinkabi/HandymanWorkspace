using HandymanProviderLibrary.Api.Stuff;
using HandymanProviderLibrary.Models.Delivery;
using Microsoft.AspNetCore.Components.Authorization;

namespace Handyman_SP_UI.Helpers
{
    public class EmployeeHelper : IEmployeeHelper
    {
        AuthenticationStateProvider _authenticationStateProvider;
        EmployeeModel _employee;
        IDeliveryEndpoint _employeeEndpoint;


        string? ErrorMsg;
        static string? userId;
        public EmployeeHelper(AuthenticationStateProvider stateProvider, IDeliveryEndpoint emplyeeEndpoint)
        {
            _authenticationStateProvider = stateProvider;
            _employeeEndpoint = emplyeeEndpoint;
        }

        async Task<string> GetUserId()
        {
            try
            {
                if (userId == null)
                {
                    var user = (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;
                    userId = user.FindFirst(u => u.Type.Contains("nameidentifier"))?.Value;
                }

            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
                userId = null;
            }
            return userId;
        }
        public async Task<EmployeeModel> LoadEmployee()
        {
            try
            {
                if (userId == null)
                {
                    userId = await GetUserId();
                }
                _employee = await _employeeEndpoint.GetEmployeeWithRatings(userId);
                return _employee;
            }
            catch (Exception ex)
            {
                userId = null;
                throw new Exception(ex.Message);

            }

        }
    }
}
