using HandymanUILibrary.Models.Auth;
using System.Threading.Tasks;

namespace HandymanUILibrary.API.Auth
{
    public interface IAuthEndpoint
    {
        Task<bool> ConfirmEmail(string userId, string code);
        Task<AuthenticatedUserModel> LoginUser(string Email, string Password);
        Task<AuthenticatedUserModel> LoginUser(string userId);
        Task<bool> LogOut();
        Task<bool> Register(string username, string password);
    }
}