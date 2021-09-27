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
        static private string UserEmail { get; set; }
        private string Token { get; set; }
        static private string Username, Password; 

        //Home page Action funtion
        public ActionResult Index()
        {

            ViewBag.Username = UserEmail;
            return View();
        }

        public ActionResult About()
        {
            
            ViewBag.Message = "Your application description page.";
            ViewBag.Username = UserEmail;
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Username = UserEmail;
            ViewBag.Message = "Your contact page.";
            return View();
        }
        
        //Login action function
        public async Task<ActionResult> SignIn(UserLoginModel model)
        {

            Username = model.Username;
            Password = model.Password;

            if (ModelState.IsValid)
            {
                _apiHepler = new APIHelper();
                var results = await _apiHepler.AuthenticateUser(Username, Password);
                Token = results.Access_Token;

                UserEmail = results.UserName;
               
                return RedirectToAction("CreateProfile");
            }
            ViewBag.Username = UserEmail;
            return View();
        }      
       

        //Create a profile action method
        public async Task<ActionResult> CreateProfile(ProfileModel profile)
        {
           
           
            if (ModelState.IsValid)
            {
                if (_apiHepler == null)
                {
                    _apiHepler = new APIHelper();

                    var results = await _apiHepler.AuthenticateUser(Username, Password);
                    var loggeduser = await _apiHepler.GetLoggedInUserInfor(results.Access_Token);
                    //ViewBag.Username = UserEmail;

                    RedirectToAction("Index");
                }
                

            }

            return View();
        }
    }
}