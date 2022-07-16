using HandymanProviderLibrary.Models;

namespace HandymanProviderLibrary.API
{
    public interface IAPIHelper
    {
        HttpClient ApiClient { get; }

        Task<AuthenticatedUserModel> AuthenticateUser(string username, string password);
        Task<IloggedInUserModel> GetLoggedInUserInfor(string Token);
        void LogOutuser();
    }
}