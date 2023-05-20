// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using HandymanUILibrary.API.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Handymen_UI_Consumer.Pages.Auth;

public class LogoutModel : PageModel
{

    private readonly ILogger<LogoutModel> _logger;
    private readonly IAuthEndpoint _authEndpoint;
    private readonly AuthenticationStateProvider _authStateProvider;

    public LogoutModel(
        ILogger<LogoutModel> logger, IAuthEndpoint authEndpoint,
        AuthenticationStateProvider authenticationStateProvider)
    {

        _logger = logger;
        _authEndpoint = authEndpoint;
        _authStateProvider = authenticationStateProvider;
    }

    public async Task<IActionResult> OnPost(string returnUrl = null)
    {

        //Manual logout
        bool IsLoggedOut = await _authEndpoint.LogOut();

        if (IsLoggedOut)
        {
            var state = await _authStateProvider.GetAuthenticationStateAsync();
        }
        _logger.LogInformation("User logged out.");
        if (returnUrl != null)
        {
            return LocalRedirect(returnUrl);
        }
        else
        {
            // This needs to be a redirect so that the browser performs a new
            // request and the identity for the user gets updated.
            return RedirectToPage();
        }
    }
}
