using System.Net.Http;
using System.Threading.Tasks;
using HandymanUILibrary.Models;

namespace HandymanUILibrary.API
{
    /// <summary>
    ///Interface for API helper 
    /// </summary>
    public interface IAPIHelper
    {
        HttpClient ApiClient { get; }
        Task<AuthenticatedUserModel> AuthenticateUser(string username, string password);
        Task<loggedInUserModel> GetLoggedInUserInfor(string token);

        void LogOutuser();

    }
}