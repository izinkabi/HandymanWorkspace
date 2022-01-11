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

        private IProfileEndPoint _profileEndPoint;
        private IRegisterEndPoint _registerEndPoint;

        static private string DisplayUserName;
        static private loggedInUserModel _loggedInUserModel;


        public LoginController(IAPIHelper aPIHelper, IProfileEndPoint profile, IRegisterEndPoint registerEndPoint, loggedInUserModel loggedInUserModel)
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

                    var result = await _apiHepler.GetLoggedInUserInfor(results.Access_Token);
                    _loggedInUserModel.Token = result.Token;
                    _loggedInUserModel.Id = result.Id;
                    _loggedInUserModel.UserRole = result.UserRole;

                    Session["log"] = results.Access_Token;
                    Token = results.Access_Token;
                    TempData["welcome"] = "Welcome " + Username;
                    Session["Username"] = Username;
                    Session["Token"] = results.Access_Token;

                    //////this will brake my code--(! Illegal assignment of user-roles from aspnetdb, less secured...  )
                    if (_loggedInUserModel.UserRole.Equals("Customer"))

                    {
                        return RedirectToAction("Index", "Service");

                    }
                    else if (_loggedInUserModel.UserRole.Equals("ServiceProvider"))
                    {
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

        //Log Out Function
        public ActionResult Logout()
        {
            //clear the instances in the container
            Session["Username"] = null;
            Session["profilename"] = null;
            _loggedInUserModel = null;

            _apiHepler.LogOutuser();
            Session["log"] = null;
            _apiHepler = null;
            return RedirectToAction("Index", "Service");

        }

    }
}