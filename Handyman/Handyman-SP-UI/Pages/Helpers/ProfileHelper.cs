using Handyman_SP_UI.Areas.Identity.Data;
using HandymanProviderLibrary.Api.EndPoints.Interface;
using HandymanProviderLibrary.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Handyman_SP_UI.Pages.Helpers;

public class ProfileHelper : PageModel
{
    private readonly IServiceProviderEndPoint _providerEndPoint;
    private readonly AuthenticationStateProvider? _authenticationStateProvider;
    private readonly UserManager<Handyman_SP_UIUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly AppUserManager _appUserManager;

    public ProfileHelper(IServiceProviderEndPoint providerEndPoint, AuthenticationStateProvider? authenticationStateProvider,
        UserManager<Handyman_SP_UIUser> userManager,
        AppUserManager appUserManager, RoleManager<IdentityRole> roleManager)
    {
        _providerEndPoint = providerEndPoint;
        _authenticationStateProvider = authenticationStateProvider;
        _userManager = userManager;
        _roleManager = roleManager;
        _appUserManager = appUserManager;
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

        var User = GetUser();
        var user = _userManager.GetUserAsync(User).Result;
        if (!(_roleManager.RoleExistsAsync("Owner").Result))
        {
            await _roleManager.CreateAsync(new IdentityRole("Owner"));
            await _userManager.AddToRoleAsync(_userManager.GetUserAsync(User).Result, "Owner");
        }
        else if (_roleManager.RoleExistsAsync("Owner").Result)
        {
            try
            {

                if (User.IsInRole("Owner"))
                {
                    return true;
                }
                else
                {
                    if (user != null)
                    {


                        //IdentityResult result = await _appUserManager.AddToRoleByRoleIdAsync(user, await _roleManager.GetRoleIdAsync(role));

                        return true;
                    }

                }



            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex.InnerException);
            }


        }
        return false;
    }

    //Register Profile
    protected internal void CreateProfile(ProfileModel newProfile)
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

    protected internal ClaimsPrincipal GetUser()
    {
        if (User != null)
        {
            return User;
        }
        return null;
    }


}


