using Handyman_DataLibrary.Internal.DataAccess;

namespace Handyman_DataLibrary.DataAccess.Query;
public class AuthData : IAuthData
{
    ISQLDataAccess? _dataAccess;

    public AuthData(ISQLDataAccess? dataAccess)
    {
        _dataAccess = dataAccess;
    }

    //Delete the token
    //Our little logout cheat 
    public void DeleteToken(string userId)
    {
        try
        {
            _dataAccess.SaveData("dbo.spJwtToken_Delete", new { userId = userId }, "Handyman_DB");
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
}
