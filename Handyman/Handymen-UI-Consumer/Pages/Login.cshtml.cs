// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using HandymanUILibrary.API.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Handymen_UI_Consumer.Pages;
[AllowAnonymous]
public class LoginModel : PageModel
{
    private readonly ILogger<LoginModel> _logger;
    private readonly IAuthEndpoint _authEndpoint;
    private readonly AuthenticationStateProvider authStateProvider;

    public LoginModel(ILogger<LoginModel> logger, IAuthEndpoint authEndpoint,
        AuthenticationStateProvider authenticationStateProvider)
    {
        _logger = logger;
        _authEndpoint = authEndpoint;
        authStateProvider = authenticationStateProvider;
    }

    /// <summary>
    /// Properties binded to the Login UI Page (incl, the input model, Extenal Login scheme and return URL)
    /// </summary>
    [BindProperty]
    public InputModel Input { get; set; }
    public IList<AuthenticationScheme> ExternalLogins { get; set; }
    public string ReturnUrl { get; set; }

    [TempData]
    public string ErrorMessage { get; set; }

    /// <summary>
    /// This is the input model for the log in Page - It is Binded to the UI Login Page
    /// </summary>
    public class InputModel
    {
        [Required]
        //[EmailAddress]
        [Display(Name = "Email/Username")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public async Task OnGetAsync(string returnUrl = null)
    {
        if (!string.IsNullOrEmpty(ErrorMessage))
        {
            ModelState.AddModelError(string.Empty, ErrorMessage);
        }

        returnUrl ??= Url.Content("/");

        // Clear the existing external cookie to ensure a clean login process
        //await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

        ReturnUrl = returnUrl;
    }

    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
        returnUrl ??= Url.Content("/");

        //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

        if (ModelState.IsValid)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: tru

            var loggedInUser = await _authEndpoint.LoginUser(Input.Email, Input.Password);
            var result = await authStateProvider.GetAuthenticationStateAsync();

            if (result.User.Identity.IsAuthenticated)
            {
                _logger.LogInformation("User logged in.");
                return LocalRedirect(returnUrl);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }
        }

        // If we got this far, something failed, redisplay form
        return Page();
    }
}
