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

       
        //this only register the user in our HandymanDB users
        public void PostUser(UserModel userModel)
        {
              UserData data = new UserData();  
              data.RegisterUser(userModel);
        }


        [Authorize(Roles = "Customer,ServiceProvider,Admin")]
        //Getting the Id on the ApiDatabase
        // GET: User/Details/3
        public UserModel GetById()
        {
            string userId = RequestContext.Principal.Identity.GetUserId();
            var userModel = new UserModel();
            //bool isRole = RequestContext.Principal.IsInRole("Customer");       
            
            userModel.Id = userId;
            UserData data = new UserData();
            var dbuser = new UserModel();
            //assigning roles(More security can be enforced)
            //this is due to the fact tha our current ui app has not auth
            //(single or azure user option on app creation)
            dbuser = data.GetUserById(userModel);
            if (User.IsInRole("Customer"))
            {
                dbuser.UserRole = "Customer";
            }
            else if (User.IsInRole("Admin"))
            {
                dbuser.UserRole = "Admin";
            }
            else if (User.IsInRole("ServiceProvider"))
            {
                dbuser.UserRole = "ServiceProvider";
            }
            else
            {
                dbuser.UserRole = null;
            }
            
            return dbuser;
        }

       
        
    }
}