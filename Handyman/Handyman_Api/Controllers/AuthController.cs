using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.DataAccess.Query;
using Handyman_DataLibrary.Helpers;
using Handyman_DataLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;


namespace Handyman_Api.Controllers;
[Route("api/Auth")]
[ApiController]
//[Authorize]
public class AuthController : ControllerBase
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ILogger<LoginModel> _logger;
    private readonly ITokenProvider _tokenProvider;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _config;
    private readonly IAuthData _authData;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IProfileData _profileData;
    private readonly EmailSender emailSender;

    public AuthController(SignInManager<IdentityUser> signInManager,
        ILogger<LoginModel> logger, ITokenProvider tokenProvider,
        UserManager<IdentityUser> userManager, IConfiguration config,
        IAuthData authData, RoleManager<IdentityRole> roleManager,
        IProfileData profileData, EmailSender emailSender)
    {
        _signInManager = signInManager;
        _logger = logger;
        _tokenProvider = tokenProvider;
        _userManager = userManager;
        _config = config;
        _authData = authData;
        _roleManager = roleManager;
        _profileData = profileData;
        this.emailSender = emailSender;
    }
    /// <summary>
    /// Login userr
    /// Authenticate the user on the JWT system
    /// Authorize the user using Identity system
    /// </summary>
    /// <param name="loginModel"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
	[HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginUser(LoginModel loginModel)
    {
        try
        {
            SignInResult result = new SignInResult();
            //Authenticate the user using Identity - Signin Manager
            //This to log in the user
            if (loginModel == null)
            {
                return BadRequest("Error login in please try again");
            }

            if (!string.IsNullOrEmpty(loginModel.UserId))
            {
                var identityUser = await _userManager.FindByIdAsync(loginModel.UserId);
                var token = "";
                if (identityUser != null)
                {
                    var claims = await _userManager.GetClaimsAsync(identityUser);
                    if (claims == null || claims.Count < 1)
                    {
                        token = _tokenProvider.GenerateToken(identityUser.Email, loginModel.UserId, loginModel.Role);
                    }
                    else
                    {
                        token = _tokenProvider.GenerateToken(claims);
                    }
                    // Generate token using JWT

                    // Set the token as the authentication token for the user
                    var identityResult = await _signInManager.UserManager.SetAuthenticationTokenAsync(identityUser, "Jwt", "Bearer", token);
                    if (identityResult.Succeeded)
                    {
                        await _signInManager.SignInWithClaimsAsync(identityUser, true, claims);
                        return Ok(token);
                    }
                    else
                        return BadRequest("Invalid login");
                }

            }

            result = await _signInManager.PasswordSignInAsync(userName: loginModel.Email, loginModel.Password, isPersistent: true, true);




            if (result.Succeeded)
            {
                _logger.LogInformation("User Logged In");
                var user = await _userManager.FindByEmailAsync(loginModel.Email ?? _signInManager.Context.User.Identity.Name);

                if (user != null)
                {
                    // var claims = (await _userManager.GetClaimsAsync(user)).ToList();
                    // Generate token using JWT
                    var token = _tokenProvider.GenerateToken(loginModel.Email ?? user.Email, loginModel.UserId ?? user.Id, loginModel.Role);
                    // Set the token as the authentication token for the user
                    var identityResult = await _signInManager.UserManager.SetAuthenticationTokenAsync(user, "Jwt", "Bearer", token);
                    return Ok(token);
                }
                return Accepted();
            }
            else if (result.IsLockedOut)
            {
                _logger.LogWarning("User Locked Out");
                return StatusCode(423);
            }
            else if (result.IsNotAllowed)
            {
                _logger.LogWarning("User Unauthorized");
                return Unauthorized();
            }
            else
            {
                _logger.LogError("Not Found");
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //Register
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterModel registerModel)
    {
        var user = new IdentityUser { UserName = registerModel.Email, Email = registerModel.Email };
        var result = await _userManager.CreateAsync(user, registerModel.Password);
        if (result.Succeeded)
        {
            _logger.LogInformation("user created with a password");

            //Assigning the User Roles 
            try
            {

                foreach (string role in registerModel.Roles)
                {
                    var Exists = await _roleManager.RoleExistsAsync(role);
                    if (Exists)
                    {
                        var response = await _userManager.AddToRoleAsync(user, role);
                    }
                    else
                    {
                        await _roleManager.CreateAsync(new IdentityRole(role));
                        var response = await _userManager.AddToRoleAsync(user, role);
                    }

                }
                var IsConfirmed = await ConfirmEmail(user, registerModel.Roles.First());
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex.InnerException);
            }
            return Ok();
        }
        else
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return BadRequest(ModelState);
        }
    }


    /// <summary>
    /// Create the user-profile
    /// </summary>
    /// <param name="profile"></param>
    // POST api/<AuthController>
    [HttpPost]
    [Route("PostProfile")]
    public void PostProfile(ProfileModel profile)
    {
        if (profile != null)
        {

            profile.EmailAddress = User.Identity.Name;
            if (string.IsNullOrEmpty(profile.UserId))
            {
                profile.UserId = _signInManager.UserManager.GetUserId(User);
            }

            _profileData.InsertProfile(profile);
        }
    }

    //Confirm the email from
    [HttpPost("ConfirmEmail")]
    [AllowAnonymous]
    public async Task<IActionResult> Confirm(string userId, string? code)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (userId == null || code == null)
        {
            return RedirectToPage("/Index");
        }

        //var user = await _userManager.FindByIdAsync(userId);
        if (userId == null)
        {
            return NotFound($"Unable to load user with ID '{userId}'.");
        }

        code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        var result = await _userManager.ConfirmEmailAsync(user, code);

        if (result.Succeeded)
        {
            return Ok();
        }
        else
        {
            return NotFound($"Problem confirming your email.");
        }
    }

    // TODO - we need to call this method from a library at the back - OR FROM THE EMAIL HANDLER/HELPER WE HAVE SET UP IN THE LIBRARY
    //Helper method for to send the email confirmation link
    private async Task<IActionResult> ConfirmEmail(IdentityUser user, string applicationRole)
    {
        try
        {
            var returnUrl = Url.Content("~/");


            if (user == null)
            {
                return NotFound($"Unable to load user with email '{user.EmailConfirmed}'.");
            }


            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            Dictionary<string, Uri?> appRoles = new Dictionary<string, Uri?>()
            {
                ["ServiceProvider"] = new Uri($"https://localhost:7042/confirm-email?userId={user.Id}&code={code}"),
                ["Consumer"] = new Uri($"https://localhost:7207/confirm-email?userId={user.Id}&code={code}")

            };
            //Initialized
            var callbackUrl = new Uri("//empty");
            if (!string.IsNullOrEmpty(applicationRole))
            {
                //foreach (KeyValuePair<string, Uri> role in appRoles)
                //{
                //    if (role.Key.ToString() == appRoles.ToString())
                //    {
                //        callbackUrl = role.Value;
                //    }
                //}

                if (appRoles.ContainsKey(applicationRole))
                {
                    callbackUrl = appRoles[applicationRole];
                }

            }
            //  = new Uri($"https://localhost:7042/confirm-email?userId={user.Id}&code={code}");//little cheat
            //"/Auth/ConfirmEmail",
            //pageHandler: null,
            //values: new { userId = user.Id, code = code},
            //protocol: Request.Scheme);

            //Email Sender Method call
            SendEmail($"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl.AbsoluteUri)}'>clicking here</a>.");


        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
        return Ok();
    }

    //Send an email using Mailkit
    private void SendEmail(string body) => emailSender.SendEmail(body);


    /// <summary>
    /// Get the user profile using an email
    /// get claims from the token to get an email that will be used to identify the user.
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpGet("userprofile")]
    [Authorize]
    public async Task<IActionResult> GetUserProfile()
    {
        try
        {
            // Get the email claim from the authenticated user's identity

            Claim? idClaim = User.FindFirst(ClaimTypes.Email);
            if (idClaim == null)
            {
                _logger.LogInformation("Email Invalid");
                //email claim not found, empty or null
                return BadRequest("Invalid Request");
            }
            //Get email value from the claim 
            var email = idClaim.Value;

            //get the user information using the email you got from the claim
            IdentityUser? user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogInformation("User unauthorized");
                return Unauthorized();
            }
            return Ok(user);
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }

    }
    /// <summary>
    /// Password reset endpoint
    /// Deal with comparing the current and new password to change user password
    /// If current password is incorrect, new password will not be set.
    /// </summary>
    /// <param name="passwrodChangeModel"></param>
    /// <returns>passwordChanged > Success OR Error message for fail</returns>
    [HttpPost("changepassword")]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] PasswrodChangeModel passwrodChangeModel)
    {
        var user = _signInManager.Context.User;
        if (_signInManager.Context.User.Identity.IsAuthenticated)
        {
            try
            {
                IdentityUser identityUser = new IdentityUser();

                Claim? EmailClaim = User.FindFirst(ClaimTypes.Email);

                var email = EmailClaim?.Value;
                identityUser = await _userManager.FindByEmailAsync(email);
                //validate the model and make sure the password meets the criteria 
                if (passwrodChangeModel != null)
                {
                    var passwordChanged = await _userManager.ChangePasswordAsync(identityUser, passwrodChangeModel.currentPassword, passwrodChangeModel.newPassword);

                    return Ok(passwordChanged);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        return Ok();
    }

    /// <summary>
    /// Reset the password for an authenticated user
    /// Need to send a Password reset token to the user to 
    /// </summary>
    /// <param name="identityUser"></param>
    /// <param name="passwordResetModel"></param>
    /// <returns>200 status code if password has been reset</returns>
    [HttpPost("resetpassword")]
    [Authorize]
    public async Task<IActionResult> ResetPassword([FromBody] PasswordResetModel passwordResetModel)
    {
        var user = _signInManager.Context.User;
        if (_signInManager.Context.User.Identity.IsAuthenticated)
        {
            try
            {
                IdentityUser identityUser = new IdentityUser();

                Claim? EmailClaim = User.FindFirst(ClaimTypes.Email);

                var email = EmailClaim?.Value;
                identityUser = await _userManager.FindByEmailAsync(email);
                //validate model
                if (identityUser != null)
                {
                    // TODO - Need to implement a email handler that will deal with sending this password reset Token 
                    var passwordresetToken = await _userManager.GeneratePasswordResetTokenAsync(identityUser);
                    if (passwordresetToken != null)
                    {
                        //var confirmedToken = emailSender.ResetPasswordEmail(identityUser.Email, passwordresetToken);
                        //Need get this Password Reset Token from an email sender 
                        //This need to be fixed. It works for now 
                        // var passwordReset = await _userManager.ResetPasswordAsync(identityUser, confirmedToken, passwordResetModel.NewPassword);

                        // return Ok(passwordReset);
                    }



                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        return Unauthorized();
    }

    //LogOut
    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        var user = _signInManager.Context.User;
        if (_signInManager.Context.User.Identity.IsAuthenticated)
        {
            try
            {
                var identityUser = await _userManager.FindByEmailAsync(user.Identity.Name);
                // var id = user.FindFirst(u => u.Type.Contains("nameidentifier"))?.Value;
                if (!string.IsNullOrEmpty(identityUser.Id))
                    _authData.DeleteToken(identityUser.Id);
                _signInManager.SignOutAsync();
            }
            catch (Exception)
            {

                throw;
            }

        }

        return Ok();
    }
}

