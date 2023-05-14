using HandymanProviderLibrary.Models;

namespace HandymanProviderLibrary.Api.EndPoints.Interface;
public interface IAuthEndpoint
{
    Task<bool> ConfirmEmail(string userId, string? code);
    Task<AuthenticatedUserModel> LoginUser(string Email, string Password);
    Task<bool> LogOut();
    Task<bool> Register(string username, string password);
}