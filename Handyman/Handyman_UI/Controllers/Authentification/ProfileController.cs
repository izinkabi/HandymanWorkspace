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
    public class ProfileController : Controller
    {

        private IAPIHelper _apiHepler;
        static private string Username,Password;
        
        private IProfileEndPoint _profileEndPoint;
        private HandymanUILibrary.Models.ProfileModel profileModel;
        private HandymanUILibrary.Models.ProfileModel.AddressModel address;
        private IloggedInUserModel _loggedInUserModel;
        

        private IRegisterEndPoint _registerEndPoint;
        private HandymanUILibrary.Models.NewUserModel user;
        static private string DisplayUserName;
       
        public ProfileController(IAPIHelper aPIHelper,IProfileEndPoint profile,IloggedInUserModel loggedInUserModel,IRegisterEndPoint registerEndPoint)
        {
            _apiHepler = aPIHelper;
            _profileEndPoint = profile;
            _registerEndPoint = registerEndPoint;
            _loggedInUserModel = loggedInUserModel;

        }
        public PartialViewResult CreateAddress()
        {
            
            return PartialView();
        }


        public async Task<ActionResult> Register(CreateUserModel newUser)
        {

            if (ModelState.IsValid)
            {
                try
                {


                    user = new HandymanUILibrary.Models.NewUserModel();

                    //Username and password are given a value once here and once in sign in
                    //that might need a credentials interface and its class

                    user.Email = newUser.Username;
                    user.Password = newUser.Password;
                    user.ConfirmPassword = newUser.ConfirmPassword;

                    Username = newUser.Username;
                    Password = newUser.Password;

                    var result = await _registerEndPoint.RegisterUser(user);

                    //save the data to the localdb of backup
                    var results = await _registerEndPoint.SaveNewUser(user);

                    DisplayUserName = result.FirstName;
                    Session["log"] = result.Email;
                    Session["profilename"] = result.Email;

                    return RedirectToAction("CreateAProfile", "Profile");

                }
                catch (Exception ex)
                {
                    ViewBag.RegisterErrorMsg = ex.Message;
                }

            }
            return View();
        }

        public async Task<ActionResult> CreateAProfile(Models.ProfileModel profile, Models.ProfileModel.AddressModel address )
        {

            if (ModelState.IsValid)
            {
                //this should be enough to pass to the API endpoint
                profileModel = new HandymanUILibrary.Models.ProfileModel();
                profileModel.Address = new HandymanUILibrary.Models.ProfileModel.AddressModel();
               
                profileModel.Name = profile.Name;
                profileModel.Surname = profile.Surname;
                profileModel.PhoneNumber = profile.PhoneNumber;
                profileModel.DateofBirth = profile.DateTimeOfBirth;
               
                profileModel.Address.StreetName = address.StreetName;
                profileModel.Address.Surburb = address.Surburb;
                profileModel.Address.PostalCode = address.PostalCode;
                profileModel.Address.HouseNumber = address.HouseNumber;
                profileModel.Address.City = address.City;

                ViewBag.profilename = profile.Name + profile.Surname;

                try
                {
                    //We await multiple tasks to create the profile
                    //First Get the token with auth and get the loggedin user
                    //Then pass the user ID but keep it to track the user 
                    //Finally we await the profile post task and if successful redict to the mentioned view/method


                    var results = await _apiHepler.AuthenticateUser(Username, Password);//auth awaited task
                    
                    var loggeuser = await _apiHepler.GetLoggedInUserInfor(results.Access_Token);// awaited loggedUser

                    profileModel.UserId = loggeuser.Id;//pass user ID

                    await _profileEndPoint.PostProfile(profileModel);//waited results of posted user profile
                    Session["profilename"] = loggeuser.Username;
                    Session["log"] = loggeuser.Username;
                    return RedirectToAction("Index","Service");

                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMsg = ex.Message;
                }

                

            }

            return View();
        }


        public async Task<ActionResult> ProfileDetails(string userId)
        {
            try
            {
                
               //Getting a profile with its Address
               var results =  await _profileEndPoint.GetProfile(userId);

                Session["log"] = results.UserId;
                ViewData["username"] = Username;
                profileModel = results;
                return View(profileModel);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
            }

            return View();
        }
    }
}