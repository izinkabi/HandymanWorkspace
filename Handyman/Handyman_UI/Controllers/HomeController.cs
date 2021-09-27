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

        private APIHelper _apiHepler;
        public string UserEmail { get; set; }
        private string Token { get; set; }
        static private string Username, Password; 

        public ActionResult Index()
        {
            ViewBag.Username = UserEmail;
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

            Username = model.Username;
            Password = model.Password;

            if (ModelState.IsValid)
            {
                _apiHepler = new APIHelper();
                var results = await _apiHepler.AuthenticateUser(Username, Password);
                Token = results.Access_Token;
                

                //ViewBag.Username = loggeduser.Email;
                //ViewBag.UsernameID = loggeduser.UserId;
                //UserEmail = loggeduser.Email;

                return RedirectToAction("CreateProfile");
            }
            return View();
        }      
       
        public async Task<ActionResult> CreateProfile(ProfileModel profile)
        {
           
           
            if (ModelState.IsValid)
            {
                if (_apiHepler == null)
                {
                    _apiHepler = new APIHelper();

                    var results = await _apiHepler.AuthenticateUser(Username, Password);
                    var loggeduser = await _apiHepler.GetLoggedInUserInfor(results.Access_Token);

                    UserEmail = loggeduser.Email;
                    ViewBag.UserEmail = UserEmail;

                    RedirectToAction("Index");
                }
                else
                {
                    ViewBag.UserEmail = UserEmail;
                }

              
                
            }

            return View();
        }
    }
}