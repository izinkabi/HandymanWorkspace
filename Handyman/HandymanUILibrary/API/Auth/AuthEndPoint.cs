using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandymanUILibrary.API.User;
using HandymanUILibrary.Models;

namespace HandymanUILibrary.API.Auth;
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

    public async Task<string> LoginUser(string Email, string Password)
    {
        string Accesstoken = string.Empty;
        try
        {
            if (Email != null || Password != null)
            {
                Accesstoken = await _apiHelper.AuthenticateUser(Email, Password);
            }
            return Accesstoken;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }

    }
}
