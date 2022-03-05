using Handyman_UI.Models;
using HandymanUILibrary.API;
using HandymanUILibrary.Models;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Handyman_UI.Controllers
{
    public class LoginController : Controller
    {
        private IAPIHelper _apiHepler;
        static private string Username, Password;
        static private string Token;
        static private string UserRole;

        private HandymanUILibrary.Models.ProfileModel profileModel;
        private NewUserModel user;
        private TestUserModel testuserModel;

        private IProfileEndPoint _profileEndPoint;
        private IRegisterEndPoint _registerEndPoint;

        static private string DisplayUserName;
        static private IloggedInUserModel _loggedInUserModel;
        static bool IsRegistered;

        public LoginController(IAPIHelper aPIHelper, IProfileEndPoint profile, IRegisterEndPoint registerEndPoint
            , loggedInUserModel loggedInUserModel)
        {
            _apiHepler = aPIHelper;
            _profileEndPoint = profile;
            _registerEndPoint = registerEndPoint;
            _loggedInUserModel = loggedInUserModel;
        }

        //Login action function

        public async Task<ActionResult> SignIn(UserLoginModel loginmodel)
        {

            Username = loginmodel.Username;
            Password = loginmodel.Password;

            if (ModelState.IsValid)
            {

                try
                {
                    var results = await _apiHepler.AuthenticateUser(Username, Password);

                    _loggedInUserModel = await _apiHepler.GetLoggedInUserInfor(results.Access_Token);

                    //session
                    Session["loggedinUser"] = _loggedInUserModel;
                   
                    
                    _loggedInUserModel = (IloggedInUserModel)Session["loggedinuser"];
                    //////this will brake my code--(! Illegal assignment of user-roles from aspnetdb, less secured...  )
                    if (_loggedInUserModel.UserRole.Equals("Customer"))
                    {
                        if (IsRegistered)
                        {
                            return RedirectToAction("CreateAProfile", "Profile");
                        }
                        return RedirectToAction("Index", "Service");

                    }
                    else if (_loggedInUserModel.UserRole.Equals("ServiceProvider"))
                    {
                        if (IsRegistered)
                        {
                            return RedirectToAction("CreateAProfile", "Profile");
                        }
                        return RedirectToAction("Home", "ServiceProviderHome");
                    }


                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMsg = ex.Message;
                }


            }


            return View();

        }

        //Register action method
        public async Task<ActionResult> Register(CreateUserModel newUser)
        {

            if (ModelState.IsValid)
            {
                try
                {


                    user = new HandymanUILibrary.Models.NewUserModel();

                    //Username and password are given a value once here and once in sign in

                    user.Email = newUser.Username;
                    user.Password = newUser.Password;
                    user.ConfirmPassword = newUser.ConfirmPassword;

                    Username = newUser.Username;
                    Password = newUser.Password;
                    user.UserRole = "Customer";//User role assignment to customer
                    UserRole = "Customer";

                    var loggedInUser = await _registerEndPoint.RegisterUser(user);
                    // Session["Token"] = loggedInUser.Access_Token;
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

        public async Task<ActionResult> RegisterProvider(CreateUserModel newUser)
        {

            if (ModelState.IsValid)
            {
                try
                {


                    user = new HandymanUILibrary.Models.NewUserModel();

                    //Username and password are given a value once here and once in sign in

                    user.Email = newUser.Username;
                    user.Password = newUser.Password;
                    user.ConfirmPassword = newUser.ConfirmPassword;

                    Username = newUser.Username;
                    Password = newUser.Password;
                    user.UserRole = "ServiceProvider";//User role assignment
                    UserRole = "ServiceProvider";
                    var loggedInUser = await _registerEndPoint.RegisterUser(user);
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
        public ActionResult Logout()
        {
            //clear the instances in the container

            try
            {
               
                Session["profilename"] = null;
                _loggedInUserModel = null;
                Session["Token"] = null;
                _apiHepler.LogOutuser();
                Session["log"] = null;
                _apiHepler = null;
                IsRegistered = false;
                Session.Clear();
                this.Dispose();
               
                
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return RedirectToAction("Index", "Service");
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