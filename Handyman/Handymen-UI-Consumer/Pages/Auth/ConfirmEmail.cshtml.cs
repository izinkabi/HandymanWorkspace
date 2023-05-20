// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using HandymanUILibrary.API.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Handymen_UI_Consumer.Pages.Auth
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly AuthenticationStateProvider authStateProvider;
        private readonly IAuthEndpoint _authEndpoint;
        List<string> Errors = new List<string>();
        public ConfirmEmailModel(AuthenticationStateProvider authenticationStateProvider,
            IAuthEndpoint authEndpoint)
        {
            authStateProvider = authenticationStateProvider;
            _authEndpoint = authEndpoint;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }
        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return NotFound("Unable to confirm your email");
            }
            try
            {
                bool IsConfirmedEmail = await _authEndpoint.ConfirmEmail(userId, code);
                if (IsConfirmedEmail)
                {
                    var user = await _authEndpoint.LoginUser(userId);
                    var state = await authStateProvider.GetAuthenticationStateAsync();
                    if (state.User != null && state.User.Identity.IsAuthenticated)
                    {
                        return LocalRedirect("/");
                    }
                }
                else
                {
                    return NotFound($"Unable to load user with ID '{userId}'.");
                }

            }
            catch (Exception ex)
            {

                Errors.Add(ex.Message);
                return NotFound($"{ex.Message}.");
            }


            return Page();
        }
    }
}
