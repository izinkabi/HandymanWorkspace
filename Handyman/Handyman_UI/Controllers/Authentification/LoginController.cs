using Handyman_UI.Models;
using HandymanUILibrary.API;
using HandymanUILibrary.Models;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Handyman_UI.Controllers
{
    public class LoginController : Controller
    {
        
        protected private string Username, Password;
        protected private string UserRole;
        string ErrorMsg;
        protected bool isLoggedIn;
       
        protected NewUserModel NewUser;

        private IAPIHelper _apiHepler;
        protected IProfileEndPoint _profileEndPoint;
        private IRegisterEndPoint _registerEndPoint;
        protected ProfileModel profileModel;
        
        static protected IloggedInUserModel _loggedInUserModel;
        protected static bool IsRegistered;

        public LoginController(IAPIHelper aPIHelper, IProfileEndPoint profile, IRegisterEndPoint registerEndPoint
            , IloggedInUserModel loggedInUserModel)
        {
            _apiHepler = aPIHelper;
            _profileEndPoint = profile;
            _registerEndPoint = registerEndPoint;
            _loggedInUserModel = loggedInUserModel;
        }

        //Login action function

        public async Task<ActionResult> SignIn(UserLoginModel model)
        {

            Username = model.Username;
            Password = model.Password;

            if (ModelState.IsValid)
            {

                try
                {
                    var results = await _apiHepler.AuthenticateUser(Username, Password); 
                    _loggedInUserModel = await _apiHepler.GetLoggedInUserInfor(results.Access_Token);

<<<<<<< HEAD
                    //_loggedInUserModel = ;
                    //_loggedInUserModel.Token = result.Token;
                    //_loggedInUserModel.Id = result.Id;
                    //_loggedInUserModel.UserRole = result.UserRole;

                    Session["LoggedinUser"] = _loggedInUserModel;
                    Session["Token"] = results.Access_Token;

                        if (Session["LoggedinUser"] == null)
                    {
                        RedirectToAction("SignIn", "Login");
=======
                    //check if the logged user is not empty
                    if (_loggedInUserModel != null)
                    {

                        isLoggedIn = true;
                        Session["log"] = "logged";
                        //Token = _loggedInUserModel.Token;
                        TempData["welcome"] = "Welcome " + Username;
                        Session["loggedinuser"] = _loggedInUserModel;//Session of the loggedinuser
                        profileModel = await _profileEndPoint.GetProfile(_loggedInUserModel.Id);
                        if (profileModel != null)
                        {
                            Session["loggedprofile"] = profileModel;
                        }
                    }

                    if (Session["loggedinuser"] == null)
                    {
                        return View();
>>>>>>> 2a2a685f4645639256f75ad2965922ee1458c077
                    }
                    else
                    {
                        //////this will brake my code--(! Illegal assignment of user-roles from aspnetdb, less secured...  )
                        if (_loggedInUserModel.UserRole.Equals("Customer"))
                        {
                            return RedirectToAction("Index", "Services");

                        }else if(_loggedInUserModel.UserRole.Equals("ServiceProvider")){
                            return RedirectToAction("Home", "ServiceProviderHome");
                        }
                        else
                        {
                            return View("ExceptionsHelper", "PageNotFound");
                        }
                    }




                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMsg = ex.Message;
                    Session.Clear();//clear session 
                }


            }


            return View();

        }

        //Register action methods
        public async Task<ActionResult> Register(NewUserModel newUser)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    newUser.UserRole = "Customer";
                    NewUser = newUser;

                    var authenticatedUser = await _registerEndPoint.RegisterUser(newUser);
                    // Session["Token"] = loggedInUser.Access_Token;
                    if (authenticatedUser != null)
                    {
                        IsRegistered = true;
                        return RedirectToAction("SignIn", "Login");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.RegisterErrorMsg = ex.Message;
                    IsRegistered = false;
                }

            }
            return View();
        }

        public async Task<ActionResult> RegisterProvider(NewUserModel newUser)
        {

            if (ModelState.IsValid)
            {
                try
                {


                   
                    newUser.UserRole = "ServiceProvider";//User role assignment
                    NewUser = newUser;
                    var loggedInUser = await _registerEndPoint.RegisterUser(newUser);
                    //Session["Token"] = loggedInUser.Access_Token;
                    IsRegistered = true;

                    return RedirectToAction("SignIn", "Login");

                }
                catch (Exception ex)
                {
                    ViewBag.RegisterErrorMsg = ex.Message;
                    IsRegistered = false;
                }

            }
            return View();
        }


        //Log Out Function
        protected ActionResult Logout()
        {
            //clear the instances in the container

            try
            {
                Session.Clear();
                isLoggedIn = false;
                _apiHepler.LogOutuser();               
                _apiHepler = null;
                IsRegistered = false;

                
            }catch(Exception ex)
            {
                ErrorMsg = ex.Message;
            }
            return RedirectToAction("LockScreen", "Login");
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        public ActionResult LockScreen()
        {
            return View();
        }

    }
}