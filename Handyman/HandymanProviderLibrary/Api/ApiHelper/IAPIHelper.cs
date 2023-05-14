using HandymanProviderLibrary.Models;

namespace HandymanProviderLibrary.Api.ApiHelper
{
    public interface IAPIHelper
    {
        HttpClient ApiClient { get; }

        Task<string> AuthenticateUser(string username, string password);
        Task<IloggedInUserModel> GetLoggedInUserInfor(string Token);
        Task<bool> LogOutuser();
    }
}