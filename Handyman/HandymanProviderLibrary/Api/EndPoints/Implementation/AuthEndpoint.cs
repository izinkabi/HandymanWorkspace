using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandymanProviderLibrary.Api.ApiHelper;
using HandymanProviderLibrary.Api.EndPoints.Interface;
using HandymanProviderLibrary.Models;
using HandymanProviderLibrary.Models.AuthModels;

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

    public async Task<AuthenticatedUserModel> LoginUser(string Email, string Password)
    {
        try
        {
            if (Email != null || Password != null)
            {
                 string? result = await _apiHelper.AuthenticateUser(Email,Password);
                _authedUser.Access_Token = result;
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
}
