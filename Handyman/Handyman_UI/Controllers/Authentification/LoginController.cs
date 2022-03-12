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
        protected ProfileModel profileModel;

        private IAPIHelper _apiHepler;
        protected IProfileEndPoint _profileEndPoint;
        private IRegisterEndPoint _registerEndPoint;              
        protected static IloggedInUserModel _loggedInUserModel;
        private static AuthenticatedUserModel authenticatedUser;
        private static bool IsRegistered;
        

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
                    if (IsRegistered)
                    {
                        if (Session["loggedinuser"] == null)
                        {
                            var result = await _apiHepler.AuthenticateUser(Username, Password);
                            _loggedInUserModel = await _apiHepler.GetLoggedInUserInfor(result.Access_Token);
                            Session["loggedinuser"] = _loggedInUserModel;
                            return RedirectToAction("CreateAProfile","Profile");
                        }
                    }
                    var results = await _apiHepler.AuthenticateUser(Username, Password);
                    _loggedInUserModel = await _apiHepler.GetLoggedInUserInfor(results.Access_Token);
                    
                    //check if the logged user is not empty
                    if (_loggedInUserModel.Id != null || authenticatedUser!=null)
                    {

                        
                        if (_loggedInUserModel.Id != null)
                        
                            profileModel = await _profileEndPoint.GetProfile(_loggedInUserModel.Id);
                            TempData["welcome"] = "Welcome " + profileModel.Name + " " +profileModel.Surname;
                            Session["loggedinuser"] = _loggedInUserModel;//Session of the loggedinuser
                            isLoggedIn = true;
                           
                        

                        if (profileModel != null)
                        {
                            Session["loggedinprofile"] = profileModel;//profile session starts this can be removed after customer implementation

                            //(! Illegal assignment of user-roles from aspnetdb, less secured in api level...  )
                            if (_loggedInUserModel.UserRole.Equals("Customer"))
                            { 
                                profileModel = await _profileEndPoint.GetProfile(_loggedInUserModel.Id);
                                return RedirectToAction("Home", "CustomerHome");
                            }
                            else if (_loggedInUserModel.UserRole.Equals("ServiceProvider"))
                            { 
                                profileModel = await _profileEndPoint.GetProfile(_loggedInUserModel.Id);
                                return RedirectToAction("Home", "ServiceProviderHome");
                            }
                        }
                        else
                        {
                            if (!IsRegistered)
                            {
                                Session.Clear();
                                return View("Register");
                            }
                        }
                    }
                    else
                    {
                        return View();
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
                    var result = await _registerEndPoint.RegisterUser(newUser);
                    authenticatedUser = (AuthenticatedUserModel)result;
                    //Session["loggedinuser"] = await _apiHepler.GetLoggedInUserInfor(result.Access_Token.ToString());
                    if (authenticatedUser != null && authenticatedUser is AuthenticatedUserModel)
                    {
                        Session["authedUser"] = authenticatedUser;
                        IsRegistered = true;
                        return RedirectToAction("SignIn");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.RegisterErrorMsg = ex.Message;
                    IsRegistered = false;
                    return View();
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
                    authenticatedUser = await _registerEndPoint.RegisterUser(newUser);

                    if (authenticatedUser != null && authenticatedUser is AuthenticatedUserModel)
                    {
                        Session["authedUser"] = authenticatedUser;
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


        //Log Out Function
        public ActionResult Logout()
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