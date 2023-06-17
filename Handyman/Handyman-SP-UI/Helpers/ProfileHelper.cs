using HandymanProviderLibrary.Api.EndPoints.Interface;
using HandymanProviderLibrary.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Handyman_SP_UI.Helpers;

public class ProfileHelper
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


    //Register Profile
    protected internal async Task<bool> CreateProfile(ProfileModel newProfile)
    {
        if (newProfile != null)
        {
            try
            {
                return await _providerEndPoint.CreateProfile(newProfile);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

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



}


