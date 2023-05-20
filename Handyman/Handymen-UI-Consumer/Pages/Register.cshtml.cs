// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using HandymanUILibrary.API.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Handymen_UI_Consumer.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IAuthEndpoint _authEndpoint;
        private readonly ILogger<RegisterModel> _logger;
        private readonly AuthenticationStateProvider _authStateProvider;
        public List<string> Errors = new List<string>();

        public RegisterModel(
            IAuthEndpoint authEndpoint,
            ILogger<RegisterModel> logger,
            AuthenticationStateProvider authenticationState)
        {
            _logger = logger;
            _authStateProvider = authenticationState;
            _authEndpoint = authEndpoint;
        }

        /// <summary>
        /// Binded Properties
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        public bool IsRegistered { get; set; }
        public string ReturnUrl { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        /// This is a Register Model Binded to the Register UI page
        /// </summary>
        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Display(Name = "Username")]
            [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            public string Username { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            // ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {

                try
                {
                    //API login
                    IsRegistered = await _authEndpoint.Register(Input.Email, Input.Password);
                    //Get app authState
                    var state = await _authStateProvider.GetAuthenticationStateAsync();
                    if (IsRegistered && state.User.Identity.IsAuthenticated)
                    {
                        _logger.LogInformation("User created a new account with password.");
                    }
                    foreach (var error in Errors)
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }
                }
                catch (Exception ex)
                {

                    Errors.Add(ex.Message);
                }

            }


            return Page();
        }



    }
}
