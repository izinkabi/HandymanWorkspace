using System.Security.Claims;

namespace Handyman_DataLibrary.DataAccess.Interfaces;

public interface ITokenProvider
{
    string GenerateToken(string email, string? userId, string? role);
    string GenerateToken(IList<Claim> claims);
}