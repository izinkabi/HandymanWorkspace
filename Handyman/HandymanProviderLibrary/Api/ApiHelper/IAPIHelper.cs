using HandymanProviderLibrary.Models;

namespace HandymanProviderLibrary.Api.ApiHelper
{
    public interface IAPIHelper
    {
        HttpClient ApiClient { get; }

        Task<string> AuthenticateUser(string username, string password);
        Task<string> AuthenticateUser(string userId);
        Task<IloggedInUserModel> GetLoggedInUserInfor(string Token);
        Task<bool> LogOutuser();
    }
}