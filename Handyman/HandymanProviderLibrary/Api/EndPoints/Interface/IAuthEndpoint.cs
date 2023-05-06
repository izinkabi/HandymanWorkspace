using HandymanProviderLibrary.Models;
using HandymanProviderLibrary.Models.AuthModels;

namespace HandymanProviderLibrary.Api.EndPoints.Interface;
public interface IAuthEndpoint
{
    Task<AuthenticatedUserModel> LoginUser(string Email, string Password);
}