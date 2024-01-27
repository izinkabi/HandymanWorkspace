using HandymanUILibrary.Models.Auth;
using System.Net.Http;
using System.Threading.Tasks;

namespace HandymanUILibrary.API.User;
public interface IAPIHelper
{
    HttpClient ApiClient { get; }

    Task<string> AuthenticateUser(string email, string password);
    Task<string> AuthenticateUser(string userId);
    Task<IloggedInUserModel> GetLoggedInUserInfor(string Token);
    Task<bool> LogOutUser();
}