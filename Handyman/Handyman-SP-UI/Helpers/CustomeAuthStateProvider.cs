using HandymanProviderLibrary.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;


namespace Handyman_SP_UI.Helpers;

public class CustomeAuthStateProvider : AuthenticationStateProvider
{

    private readonly AuthenticatedUserModel _authuserModel;

    public CustomeAuthStateProvider(AuthenticatedUserModel authenticatedUserModel)
    {
        _authuserModel = authenticatedUserModel;

    }

    //Get the Authentication state of the token 
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = new ClaimsIdentity();
        if (!string.IsNullOrEmpty(_authuserModel.Access_Token))
        {
            identity = new ClaimsIdentity(ParseClaimsFromJwt(_authuserModel.Access_Token), "jwt");
        }
        var user = new ClaimsPrincipal(identity);
        AuthenticationState? state = new AuthenticationState(user);

        NotifyAuthenticationStateChanged(Task.FromResult(state));
        return state;
    }
    public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}
