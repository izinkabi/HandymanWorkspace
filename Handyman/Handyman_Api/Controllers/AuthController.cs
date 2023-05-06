using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Handyman_DataLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Handyman_DataLibrary.DataAccess.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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
    
    public AuthController(SignInManager<IdentityUser> signInManager, ILogger<LoginModel> logger,ITokenProvider tokenProvider, UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _logger = logger;
        _tokenProvider = tokenProvider;
        _userManager = userManager;

    }
    //Login
    [HttpPost("login")]
    [AllowAnonymous ]

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
        if(result.Succeeded)
        {
            _logger.LogInformation("user created with a password");

            //var token = _tokenProvider.GenerateToken(registerModel.Email);
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
        await _signInManager.SignOutAsync();
        return Ok();
    }  
}
