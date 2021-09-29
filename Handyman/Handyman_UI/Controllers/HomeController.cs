using Handyman_UI.Models;
using HandymanUILibrary.API;
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
        static private string UserId;


        static private string Token { get; set; }
        static private string Username, Password;
        static private int ProfileId;
        private ProfileEndPoint profileEndPoint;
        private bool loggedIn;
       



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
                ViewBag.Username = Username;
                
                return RedirectToAction("CreateProfile","Home");
               
            }
          
           
        }
        

        //Create a profile action method

        public async Task<ActionResult> CreateProfile(ProfileModel profile)
        {
            HandymanUILibrary.Models.ProfileModel profileModel = new HandymanUILibrary.Models.ProfileModel();
            profileModel.Name = profile.Name;
            profileModel.Surname = profile.Surname;
            profileModel.PhoneNumber = profile.PhoneNumber;
            profileModel.HomeAddress = profile.HomeAddress;
            profileModel.DateofBirth = profile.DateTimeOfBirth;
            profileModel.Type = "user";

            if (ModelState.IsValid)
            {
                if (_apiHepler == null)
                {
                    _apiHepler = new APIHelper();
                    profileEndPoint = new ProfileEndPoint(_apiHepler);


                    var results = await _apiHepler.AuthenticateUser(Username, Password);           
                    Token = results.Access_Token;
                    var loggeduser = await _apiHepler.GetLoggedInUserInfor(Token);
                    profileModel.UserId = loggeduser.Id;

                    await profileEndPoint.PostProfile(profileModel);
                   return RedirectToAction("GetOTP");

                }
                
     
            }

            return View("Index");
        }

        public ActionResult GetOTP()
        {


            return View();
        }

        public ActionResult Logout()
        {
            return View();
        }
    }
}