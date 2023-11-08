namespace Handyman_Api.Controllers
{

    //public class TwoFactorAuthenticationModel
    //{
    //    private readonly UserManager<IdentityUser> _userManager;
    //    private readonly SignInManager<IdentityUser> _signInManager;
    //    private readonly ILogger<TwoFactorAuthenticationModel> _logger;

    //    public TwoFactorAuthenticationModel(
    //        UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<TwoFactorAuthenticationModel> logger)
    //    {
    //        _userManager = userManager;
    //        _signInManager = signInManager;
    //        _logger = logger;
    //    }

    //    /// <summary>
    //    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    //    ///     directly from your code. This API may change or be removed in future releases.
    //    /// </summary>
    //    public bool HasAuthenticator { get; set; }

    //    /// <summary>
    //    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    //    ///     directly from your code. This API may change or be removed in future releases.
    //    /// </summary>
    //    public int RecoveryCodesLeft { get; set; }

    //    /// <summary>
    //    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    //    ///     directly from your code. This API may change or be removed in future releases.
    //    /// </summary>
    //    public bool Is2faEnabled { get; set; }

    //    /// <summary>
    //    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    //    ///     directly from your code. This API may change or be removed in future releases.
    //    /// </summary>
    //    public bool IsMachineRemembered { get; set; }

    //    /// <summary>
    //    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    //    ///     directly from your code. This API may change or be removed in future releases.
    //    /// </summary>
    //    public string StatusMessage { get; set; }

    //    public async Task<string> OnGetAsync()
    //    {
    //        var user = await _userManager.GetUserAsync(User);
    //        if (user == null)
    //        {
    //            return $"Unable to load user with ID '{_userManager.GetUserId(User)}'.";
    //        }

    //        HasAuthenticator = await _userManager.GetAuthenticatorKeyAsync(user) != null;
    //        Is2faEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
    //        IsMachineRemembered = await _signInManager.IsTwoFactorClientRememberedAsync(user);
    //        RecoveryCodesLeft = await _userManager.CountRecoveryCodesAsync(user);

    //        return Page();
    //    }

    //    public async Task<string> OnPostAsync()
    //    {
    //        var user = await _userManager.GetUserAsync(User);
    //        if (user == null)
    //        {
    //            return $"Unable to load user with ID '{_userManager.GetUserId(User)}'.";
    //        }

    //        await _signInManager.ForgetTwoFactorClientAsync();
    //        StatusMessage = "The current browser has been forgotten. When you login again from this browser you will be prompted for your 2fa code.";
    //        return StatusMessage;
    //    }
    //}
}
