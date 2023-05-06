using HandymanUILibrary.Models;

namespace HandymanUILibrary.API.Auth;

public interface IAuthEndpoint
{
    System.Threading.Tasks.Task<string> LoginUser(string Email, string Password);
}