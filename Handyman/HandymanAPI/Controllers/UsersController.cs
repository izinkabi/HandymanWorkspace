using HandymanDataLibrary.Models;
using HandymanDataLibrary.Internal;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace HandymanAPI.Controllers
{
    public class UsersController : ApiController
    {
        //POST: User/Details/
        public void PostUser(string email)
        {
            UserData data = new UserData();
        
                data.RegisterUser(email);
            

        }


        [Authorize]
        //Getting the Id on the ApiDatabase
        // GET: User/Details/3
        public UserModel GetById()
        {
            string userId = RequestContext.Principal.Identity.GetUserId();
            UserData data = new UserData();
            return data.GetUserById(userId);
        }

       
        
    }
}