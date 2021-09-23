using HandymanDataLibrary.Models;
using HandymanDataLibrary.Internal;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace HandymanAPI.Controllers
{
    public class UsersController : ApiController
    {
        [Authorize]
        //Getting the Id on the ApiDatabase
        // GET: User/Details/3
        public UserModel GetById(string userId)
        {
            //string userId = RequestContext.Principal.Identity.GetUserId();
            UserData data = new UserData();
            return data.GetUserById(userId);
        }

        //POST: User/Details/
        public void PostUser(UserModel usermodel)
        {
            UserData data = new UserData();
            if(data!=null)
            data.PostUser(usermodel);
        }

        
    }
}