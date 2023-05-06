using System.Net.Http;
using System.Threading.Tasks;
using HandymanUILibrary.Models;

namespace HandymanUILibrary.API.User;
public interface IAPIHelper
{
    HttpClient ApiClient { get; }

    Task<string> AuthenticateUser(string email, string password);
    Task<IloggedInUserModel> GetLoggedInUserInfor(string Token);
    void LogOutuser();
}