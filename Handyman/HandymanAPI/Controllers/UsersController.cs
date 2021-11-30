using HandymanDataLibrary.Models;
using HandymanDataLibrary.Internal;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using System.Web.Security;

namespace HandymanAPI.Controllers
{
    public class UsersController : ApiController
    {

        //POST: User/Details/

       
        public void PostUser(UserModel userModel)
        {
            UserData data = new UserData();
            //if (!Roles.RoleExists("Administrators"))
            //    // Create the role
               
            //Roles.CreateRole("Administrators");
            //Roles.AddUserToRole(userModel.Email, "Administrators");

                data.RegisterUser(userModel);
        }


        [Authorize]
        //Getting the Id on the ApiDatabase
        // GET: User/Details/3
        public UserModel GetById()
        {
            string userId = RequestContext.Principal.Identity.GetUserId();
            var userModel = new UserModel();
            userModel.Id = userId;
            UserData data = new UserData();
            return data.GetUserById(userModel);
        }

       
        
    }
}