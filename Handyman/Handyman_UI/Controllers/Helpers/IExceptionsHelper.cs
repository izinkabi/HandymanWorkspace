namespace Handyman_UI.Controllers.Helpers
{
    public interface IExceptionsHelper
    {


         void DisplayEmptyArrayError(string message);



         void DisplayNullReferenceError(string msg);



         void UnauthorizedUserError();
       
    }
}