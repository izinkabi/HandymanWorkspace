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
    public class ProfileController : Controller
    {

        private IAPIHelper _apiHepler;
        private string Username,Password;
        private IloggedInUserModel _loggedInUserModel;
        private IProfileEndPoint _profileEndPoint;
        public ProfileController(IAPIHelper aPIHelper,IProfileEndPoint profile,IloggedInUserModel loggedInUserModel)
        {
            _apiHepler = aPIHelper;
            _profileEndPoint = profile;
            _loggedInUserModel = loggedInUserModel;
        }
        public async Task<ActionResult> CreateAProfile(Models.ProfileModel profile)
        {

            if (ModelState.IsValid)
            {

                HandymanUILibrary.Models.ProfileModel profileModel = new HandymanUILibrary.Models.ProfileModel();
                profileModel.Name = profile.Name;
                profileModel.Surname = profile.Surname;
                profileModel.PhoneNumber = profile.PhoneNumber;
                profileModel.HomeAddress = profile.HomeAddress;
                profileModel.DateofBirth = profile.DateTimeOfBirth;
                profileModel.Type = "user";
                ViewBag.profilename = profile.Name + profile.Surname;

                try
                {
                    //We await multiple tasks to create the profile
                    //First Get the token with auth and get the loggedin user
                    //Then pass the user ID but keep it to track the user 
                    //Finally we await the profile post task and if successful redict to the mentioned view/method


                    var results = await _apiHepler.AuthenticateUser(Username, Password);//auth awaited task

                     await _apiHepler.GetLoggedInUserInfor(results.Access_Token);// awaited loggedUser

                    //profileModel.UserId = _loggedInUserModel.Id;//pass user ID

                    await _profileEndPoint.PostProfile(profileModel);//waited results of posted user profile
                    Session["profilename"] = results.UserName;
                    Session["log"] = results.UserName;
                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMsg = ex.Message;
                }

                

            }

            return View();
        }
    }
}