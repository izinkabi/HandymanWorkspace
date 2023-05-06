

using System.Security.Claims;
using System.Text.Json;
using HandymanProviderLibrary.Models;
using Microsoft.AspNetCore.Components.Authorization;


namespace Handyman_SP_UI;

public class CustomeAuthStateProvider : AuthenticationStateProvider
{

    private readonly AuthenticatedUserModel _authuserModel;

    public CustomeAuthStateProvider(AuthenticatedUserModel authuserModel)
    {
        _authuserModel = authuserModel;
    }

    //Get the Authentication state of the token 
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {

        //string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJuaW5qYUBtYWlsLmNvbSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJuaW5qYUBtYWlsLmNvbSIsImV4cCI6MTY4MjkzOTkwM30.EvuC2kqi1YDOiClj8Dh_vFhmIeAQkypNbs4YXXdjvAE";
        

        var identity = new ClaimsIdentity(ParseClaimsFromJwt(_authuserModel.Access_Token),"jwt");
 
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
