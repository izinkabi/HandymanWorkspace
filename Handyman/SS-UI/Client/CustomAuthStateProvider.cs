using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;


namespace SS_UI;

public class CustomAuthStateProvider : AuthenticationStateProvider
{

    //private readonly AuthenticatedUserModel _authuserModel;
    private readonly ILocalStorageService _localStorage;

    public CustomAuthStateProvider(ILocalStorageService localStorage)
    {
        //_authuserModel = authenticatedUserModel;
        _localStorage = localStorage;
    }

    //Get the Authentication state of the token 
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string? token = await _localStorage.GetItemAsStringAsync("token");
        var identity = new ClaimsIdentity();
        if (!string.IsNullOrEmpty(token))
        {
            identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
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
