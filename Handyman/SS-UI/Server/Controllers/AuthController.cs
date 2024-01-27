using HandymanUILibrary.API.User;
using HandymanUILibrary.Models.Auth;
using HandymanUILibrary.Models.AuthModels;
using Microsoft.AspNetCore.Mvc;
using SS_UI.Shared;

namespace SS_UI.Server.Controllers;


[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{

    //Local Variables for the inherited classes
    readonly IAPIHelper _apiHelper;
    private AuthenticatedUserModel _authedUser;

    public AuthController(IAPIHelper apiHelper, AuthenticatedUserModel authedUser)
    {
        _apiHelper = apiHelper;
        _authedUser = authedUser;
    }

    /// <summary>
    /// User is logging here using Jwt token as a return type
    /// </summary>
    /// <param name="Email"></param>
    /// <param name="Password"></param>
    /// <returns>JwtToken</returns>
    /// <exception cref="Exception"></exception>
    [HttpPost]
    public async Task<AuthenticatedUserModel> LoginUser(UserLoginModel loginModel)
    {
        try
        {
            if (loginModel.Username != null || loginModel.Password != null)
            {
                //authenticate and get the Authenticated user model
                string? result = await _apiHelper.AuthenticateUser(loginModel.Username, loginModel.Password);
                _authedUser.Access_Token = result;//our precious Jwt Token ;)
                //get the logged in user's profile
                var loggedInUser = await _apiHelper.GetLoggedInUserInfor(result);
                _authedUser.UserName = loggedInUser.Username;
            }

            return _authedUser;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }

    }


    /// <summary>
    /// Overloading the Login Function to use a user Id 
    /// this is mainly for redirecting after confirming the password
    /// hence it allows the newly confirmed user to immediately 
    /// jump to creating a new Workshop right after sign up
    /// </summary>
    /// <returns> An Authenticated User Model</returns>
    public async Task<AuthenticatedUserModel> LoginUser(string? userId)
    {
        try
        {
            if (!string.IsNullOrEmpty(userId))
            {
                string? result = await _apiHelper.AuthenticateUser(userId);
                _authedUser.Access_Token = result;//our precious Jwt Token ;)
                                                  //get the logged in user's profile
                var loggedInUser = await _apiHelper.GetLoggedInUserInfor(result);
                _authedUser.UserName = loggedInUser.Username;
            }

            return _authedUser;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }



    //Log the user out
    public async Task<bool> LogOut()
    {
        if (_authedUser == null) { return true; }
        if (_authedUser != null && _authedUser.Access_Token != null)
        {
            //remove the token
            await _apiHelper.LogOutUser();
            //release variables
            _authedUser.Access_Token = null;
            _authedUser = new AuthenticatedUserModel();
            return true;

        }
        else { return false; }
    }

    //Register a new user
    public async Task<bool> Register(string username, string password)
    {
        try
        {

            RegisterModel newUser = new RegisterModel
            {

                Email = username,
                Password = password,
                ConfirmPassword = password,
                Roles = { "ServiceProvider" }
            };
            var response = await _apiHelper.ApiClient.PostAsJsonAsync<RegisterModel>("auth/register", newUser);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message, ex.InnerException);
        }
    }


    public async Task<bool> ConfirmEmail(string userId, string? code)
    {
        try
        {
            var response = await _apiHelper.ApiClient.PostAsJsonAsync($"auth/confirmemail?userId={userId}&code={code}", new { });
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
}
