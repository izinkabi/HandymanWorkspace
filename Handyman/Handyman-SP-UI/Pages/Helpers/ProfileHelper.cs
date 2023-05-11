using HandymanProviderLibrary.Api.EndPoints.Interface;
using HandymanProviderLibrary.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Handyman_SP_UI.Pages.Helpers;

public class ProfileHelper : PageModel
{
    private readonly IServiceProviderEndPoint _providerEndPoint;
    private readonly AuthenticationStateProvider? _authenticationStateProvider;


    public ProfileHelper(IServiceProviderEndPoint providerEndPoint, AuthenticationStateProvider? authenticationStateProvider
       )
    {
        _providerEndPoint = providerEndPoint;
        _authenticationStateProvider = authenticationStateProvider;

    }

    protected string? userId;
    private ClaimsPrincipal User;

    //Get the profile of logged in user
    protected internal async Task<ProfileModel> GetProfile()
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

    //Create a new role in AspNetRoles(owner) and add the users
    protected internal async Task<bool> CreateOwner()
    {




        return false;
    }

    //Register Profile
    protected internal async Task<bool> CreateProfile(ProfileModel newProfile)
    {
        if (newProfile != null)
        {
            try
            {
                return await _providerEndPoint.CreateProfile(newProfile);
            }
            catch (Exception)
            {

                throw;
                return false;
            }
        }
        else
        {
            return false;
        }
    }



    /// <summary>
    /// This method gets the employee's user ID / the ID of the logged in user
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    protected internal async Task<string>? GetUserId()
    {
        try
        {
            if (userId == null)
            {
                var claim = await _authenticationStateProvider.GetAuthenticationStateAsync();

                User = claim.User;

                userId = User.FindFirst(u => u.Type.Contains("nameidentifier"))?.Value;
            }
            return userId;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected internal ClaimsPrincipal GetUser()
    {
        if (User != null)
        {
            return User;
        }
        return null;
    }


}


