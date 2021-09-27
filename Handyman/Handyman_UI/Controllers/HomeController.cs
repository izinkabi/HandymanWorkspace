using Handyman_UI.Models;
using HandymanUILibrary.API;
using HandymanUILibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Handyman_UI.Controllers
{
    public class HomeController : Controller
    {

        public APIHelper _apiHepler;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public async Task<ActionResult> SignIn(UserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                APIHelper aPIHelper = new APIHelper();
                var results = await aPIHelper.AuthenticateUser(model.Username, model.Password);

                var loggeduser =  await aPIHelper.GetLoggedInUserInfor(results.Access_Token);

                ViewBag.Username = loggeduser.Email;
                ViewBag.UsernameID = loggeduser.UserId;
                RedirectToAction("Index");
                
            }
            return View();
        }      
       
    }
}