using System.Net.Http;

namespace HandymanUILibrary.API.User
{
    /// <summary>
    ///Interface for API helper 
    /// </summary>
    public interface IAPIHelper
    {
        HttpClient ApiClient { get; }
        //Task<AuthenticatedUserModel> AuthenticateUser(string username, string password);
        //Task<IloggedInUserModel> GetLoggedInUserInfor(string token);

        void LogOutuser();

    }
}