using Microsoft.AspNetCore.Components.Authorization;

namespace Handyman_SP_UI;
public interface ICustomeAuthStateProvider
{
    Task<AuthenticationState> GetAuthenticationStateAsync(string token);
}