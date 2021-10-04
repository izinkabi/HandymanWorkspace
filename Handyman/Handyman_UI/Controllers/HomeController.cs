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
        static private string Username;
        static private string Password;
        static private int ProfileId;
        private ProfileEndPoint profileEndPoint;
        private bool loggedIn;
       



        //Home page Action funtion
        public  async Task<ActionResult> Index()
        {
          
            //ViewBag.Username = UserEmail;
            return View();
        }

        public ActionResult About()
        {
            
            ViewBag.Message = "Your application description page.";
            //ViewBag.Username = UserEmail;
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
                try
                {
                    var results = await _apiHepler.AuthenticateUser(Username, Password);
                    Token = results.Access_Token;
                    UserEmail = Username;
                    ViewBag.Username = UserEmail;
                    Session["log"] = Username;
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMsg = ex.Message;
                }
               
               
            }
            return View();
           
        }


        //RegisterUser helper method
        //saving a user in our local DB

        
       /* public async Task SaveUser()
        {
            RegisterEndPoint registerUser = new RegisterEndPoint(_apiHepler);
           
                try
                {
                    _apiHepler = new APIHelper();
                    registerUser = new RegisterEndPoint(_apiHepler);
                    var results = await registerUser.SaveNewUser(UserEmail);
                     RedirectToAction("CreateProfile");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMsgCode = ex.Message;
                }
            

        }*/

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


                    try
                    {
                        
                       var results = await _apiHepler.AuthenticateUser(Username, Password);
                        Token = results.Access_Token;
                        var loggeduser = await _apiHepler.GetLoggedInUserInfor(Token);
                        profileModel.UserId = loggeduser.Id;

                        await profileEndPoint.PostProfile(profileModel);
                        
                        return RedirectToAction("GetOTP");

                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorMsg = ex.Message;
                    }
                }
                else
                {
                    var loggeduser = await _apiHepler.GetLoggedInUserInfor(Token);
                    profileModel.UserId = loggeduser.Id;

                    await profileEndPoint.PostProfile(profileModel);
                    return RedirectToAction("GetOTP");
                }
                

            }

            return View();
        }


       
        public async Task<ActionResult> Register(CreateUserModel newUser)
        {
           
            if (ModelState.IsValid)
            {
                try {
                    

                    HandymanUILibrary.Models.NewUserModel user = new HandymanUILibrary.Models.NewUserModel();

                    user.Email = newUser.Username;
                    UserEmail = newUser.Username;
                    Username = UserEmail;
                    Password = newUser.Password;
                    user.Password = newUser.Password;
                    user.ConfirmPassword = newUser.ConfirmPassword;
                    _apiHepler = new APIHelper();

                    RegisterEndPoint registerUser = new RegisterEndPoint(_apiHepler);

                //var tasks = new Task[] {
                //          registerUser.RegisterUser(user),
                //            registerUser.SaveNewUser(user)
                // };
                //   await Task.WhenAll(tasks);

                    var result = await registerUser.RegisterUser(user);
                   
                    var results = await registerUser.SaveNewUser(user);
                    
                    UserEmail = Username;
                    //await SaveUser();
                    Session["log"] = Username;
                    
                    
                    return RedirectToAction("CreateProfile");
                    
                }
                catch (Exception ex)
                {
                    ViewBag.RegisterErrorMsg = ex.Message;
                }
            }
                return View();
        }


       

        public async Task<ActionResult> GetOTP()
        {
          
            return View();
        }

        public ActionResult Logout()
        {
            _apiHepler = new APIHelper();

                _apiHepler.LogOutuser();
                return RedirectToAction("Index");
          
        }
    }
}