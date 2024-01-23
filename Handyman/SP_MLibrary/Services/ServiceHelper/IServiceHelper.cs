using SP_MLibrary.Models;

namespace SP_MLibrary.Services.ServiceHelper;

public interface IServiceHelper
{
    HttpClient ServicesClient { get; }

    Task<string> AuthenticateUser(string username, string password);
    Task<string> AuthenticateUser(string userId);
    Task<IloggedInUserModel> GetLoggedInUserInfor(string Token);
    void InitializeClient(string token);
    Task<bool> LogOutuser();
}