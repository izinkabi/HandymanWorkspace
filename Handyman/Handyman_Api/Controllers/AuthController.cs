using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Handyman_DataLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Handyman_DataLibrary.DataAccess.Interfaces;

namespace Handyman_Api.Controllers;
[Route("api/Auth")]
[ApiController]
[Authorize]
public class AuthController : ControllerBase
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ILogger<LoginModel> _logger;
    private readonly ITokenProvider _tokenProvider;
    public AuthController(SignInManager<IdentityUser> signInManager, ILogger<LoginModel> logger,ITokenProvider tokenProvider)
    {
        _signInManager = signInManager;
        _logger = logger;
        _tokenProvider = tokenProvider;
    }

    [HttpPost("login")]
    [AllowAnonymous ]
    public async Task<IActionResult> LoginUser(LoginModel loginModel)
    {
        //Authenticate the user using Identity - Signin Manager
        //This to log in the user
        var result = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, isPersistent: false, false);
        if (result.Succeeded)
        {
            _logger.LogInformation("User Logged In");
          // Generate token using JWT
        var token = _tokenProvider.GenerateToken(loginModel.Email);
            return Ok(new { token });
        }
        else if (result.IsLockedOut)
        {
            _logger.LogWarning("User Locked Out");
            return StatusCode(423);
        }
        else if (result.IsNotAllowed)
        {
            _logger.LogWarning("User Unauthorized");
            return StatusCode(401);
        }
        else
        {
            _logger.LogError("Not Found");
            return StatusCode(404);
        }
    }


    //Register

    [HttpPost("logout")]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }
}
