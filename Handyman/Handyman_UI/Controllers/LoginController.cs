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
    public class LoginController : Controller
    {
         private string Username;
         private string Password;
        private IAPIHelper _apiHepler;

        private string DisplayUserName;


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
                    //Token = results.Access_Token;

                    //DisplayUserName = _loggedInUserModel.Username;
                    await _apiHepler.GetLoggedInUserInfor(results.Access_Token);
                    DisplayUserName = results.UserName;
                    Session["log"] = results.Access_Token;
                    Session["profilename"] = DisplayUserName;
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMsg = ex.Message;
                }


            }
            ViewBag.Username = DisplayUserName;

            return View();

        }
    }
}