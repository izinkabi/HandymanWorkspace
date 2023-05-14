using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.DataAccess.Query;
using Handyman_DataLibrary.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using MimeKit;
using MimeKit.Text;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace Handyman_Api.Controllers;
[Route("api/Auth")]
[ApiController]
[Authorize]
public class AuthController : ControllerBase
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ILogger<LoginModel> _logger;
    private readonly ITokenProvider _tokenProvider;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _config;
    private readonly IAuthData _authData;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthController(SignInManager<IdentityUser> signInManager,
        ILogger<LoginModel> logger, ITokenProvider tokenProvider,
        UserManager<IdentityUser> userManager, IConfiguration config,
        IAuthData authData, RoleManager<IdentityRole> roleManager)
    {
        _signInManager = signInManager;
        _logger = logger;
        _tokenProvider = tokenProvider;
        _userManager = userManager;
        _config = config;
        _authData = authData;
        _roleManager = roleManager;
    }
    //Login
    [HttpPost("login")]
    [AllowAnonymous]

    public async Task<IActionResult> LoginUser(LoginModel loginModel
        )
    {
        try
        {
            //Authenticate the user using Identity - Signin Manager
            //This to log in the user
            var result = await _signInManager.PasswordSignInAsync(userName: loginModel.Email, loginModel.Password, isPersistent: true, true);
            if (result.Succeeded)
            {
                _logger.LogInformation("User Logged In");
                var user = await _userManager.FindByEmailAsync(loginModel.Email);

                if (user != null)
                {
                    // Generate token using JWT
                    var token = _tokenProvider.GenerateToken(user.Email);
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

            throw ex;
        }
    }

    //Register
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterUser(RegisterModel registerModel)
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
                        await _userManager.AddToRoleAsync(user, role);
                    }
                    else
                    {
                        await _roleManager.CreateAsync(new IdentityRole(role));
                    }

                }
                await ConfirmEmail(user);
            }
            catch (Exception)
            {

                throw;
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

    private async Task<IActionResult> ConfirmEmail(IdentityUser user)
    {
        try
        {
            var returnUrl = Url.Content("~/");


            if (user == null)
            {
                return NotFound($"Unable to load user with email '{user.EmailConfirmed}'.");
            }
            //var user = await _userManager.GetUserAsync(claims);

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = new Uri($"https://localhost:7042/confirm-email?userId={user.Id}&code={code}");
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
    private void SendEmail(string body)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse("leland.thompson@ethereal.email"));
        email.To.Add(MailboxAddress.Parse("leland.thompson@ethereal.email"));
        email.Subject = "Confirm Email";
        email.Body = new TextPart(TextFormat.Html) { Text = body };

        using var smtp = new SmtpClient();
        smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
        smtp.Authenticate("leland.thompson@ethereal.email", "apxfGvfh9KnPngSGNe");
        smtp.Send(email);
        smtp.Disconnect(true);
    }

    //Get User Profile
    [HttpGet("userprofile")]
    [Authorize]
    public async Task<IActionResult> GetUserProfile()
    {
        try
        {
            // Get the email claim from the authenticated user's identity

            var emailClaim = User.FindFirst(ClaimTypes.Email);
            if (emailClaim == null)
            {
                _logger.LogInformation("Email Invalid");
                //email claim not found, empty or null
                return BadRequest("Invalid Request");
            }
            //Get email value from the claim 
            var email = emailClaim.Value;

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
                var User = await _userManager.FindByEmailAsync(user.Identity.Name);
                // var id = user.FindFirst(u => u.Type.Contains("nameidentifier"))?.Value;
                if (!string.IsNullOrEmpty(User.Id))
                    _authData.DeleteToken(User.Id);
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
