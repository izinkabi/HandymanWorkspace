using HandymanProviderLibrary.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Handyman_SP_UI.Pages.Helpers;

public class EmployeeHelper
{
    private AuthenticationStateProvider? _authenticationStateProvider;
    protected EmployeeModel? employeeModel;
    protected string? userId;
    private ClaimsPrincipal User;


    public EmployeeHelper(AuthenticationStateProvider stateProvider)
    {
        _authenticationStateProvider = stateProvider;
    }

    public EmployeeModel Employee { get => _ = Employee; }

    /// <summary>
    /// This method gets the employee's user ID / the ID of the logged in user
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    protected async Task<string>? GetUserId()
    {
        try
        {
            if (userId == null)
            {
                User = (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;
                userId = User.FindFirst(u => u.Type.Contains("nameidentifier"))?.Value;
            }
            return userId;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected ClaimsPrincipal GetUser()
    {
        if (User != null)
        {
            return User;
        }
        return null;
    }

}
