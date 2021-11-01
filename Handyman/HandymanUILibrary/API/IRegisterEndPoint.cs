using HandymanUILibrary.Models;
using System.Threading.Tasks;

namespace HandymanUILibrary.API
{
    public interface IRegisterEndPoint
    {


         Task<IloggedInUserModel> RegisterUser(NewUserModel newUser);
         Task<IloggedInUserModel> SaveNewUser(NewUserModel userModel);

    }
}