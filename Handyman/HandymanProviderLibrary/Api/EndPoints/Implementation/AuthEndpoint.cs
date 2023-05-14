using HandymanProviderLibrary.Api.ApiHelper;
using HandymanProviderLibrary.Api.EndPoints.Interface;
using HandymanProviderLibrary.Models;
using HandymanProviderLibrary.Models.AuthModels;
using System.Net.Http.Json;

namespace HandymanProviderLibrary.Api.EndPoints.Implementation;
public class AuthEndpoint : IAuthEndpoint
{
    //Local Variables for the inherited classes
    readonly IAPIHelper _apiHelper;
    private AuthenticatedUserModel _authedUser;


    public AuthEndpoint(IAPIHelper apiHelper, AuthenticatedUserModel authedUser)
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
    public async Task<AuthenticatedUserModel> LoginUser(string Email, string Password)
    {
        try
        {
            if (Email != null || Password != null)
            {
                //authenticate and get the Authenticated user model
                string? result = await _apiHelper.AuthenticateUser(Email, Password);
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
    public async Task<bool> LogOut() => await _apiHelper.LogOutuser();

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
