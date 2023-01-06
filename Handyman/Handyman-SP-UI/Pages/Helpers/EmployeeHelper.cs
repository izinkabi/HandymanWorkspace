using HandymanProviderLibrary.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace Handyman_SP_UI.Pages.Helpers;

public class EmployeeHelper
{
    AuthenticationStateProvider? _authenticationStateProvider;
    protected EmployeeModel? employeeModel;
    protected string? userId;

    public EmployeeHelper(AuthenticationStateProvider stateProvider)
    {
        _authenticationStateProvider = stateProvider;
    }

    public EmployeeModel Employee { get => _ = Employee; }

    /// <summary>
    /// This method get the employee's user ID
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    protected async Task<string>? GetUserId()
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
