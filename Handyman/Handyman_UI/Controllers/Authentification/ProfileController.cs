﻿using Handyman_UI.Models;
using HandymanUILibrary.API;
using HandymanUILibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Handyman_UI.Controllers
{
    public class ProfileController : Controller
    {

        private IAPIHelper _apiHepler;
        static private string Username,Password;
        static private string Token;
        
        private HandymanUILibrary.Models.ProfileModel profileModel;      
        private NewUserModel user;

        private IProfileEndPoint _profileEndPoint;
        private IRegisterEndPoint _registerEndPoint;
       
        static private string DisplayUserName;
        static private IloggedInUserModel _IloggedInUserModel;
        public ProfileController(IAPIHelper aPIHelper,IProfileEndPoint profile,IRegisterEndPoint registerEndPoint)
        {
            _apiHepler = aPIHelper;
            _profileEndPoint = profile;
            _registerEndPoint = registerEndPoint;         
        }

        //Action method for 
        public PartialViewResult CreateAddress()
        {
            
            return PartialView();
        }
        //--this requires auth 
        //private void createCustomerRole(string username, string password)
        //{
        //    MembershipCreateStatus createStatus;
        //    MembershipUser newUser = Membership.CreateUser(username, password);
        //    if (newUser.IsApproved)
        //    {

        //        if (!Roles.RoleExists("Customer"))
        //        {
        //            //Create the role

        //            Roles.CreateRole("Customer");
        //            Roles.AddUserToRole(newUser.UserName, "Customer");
        //        }
        //    }

        //}
        //
            
            //Register action method
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

                   
                    ViewBag.Username = result.Email; 

                    return RedirectToAction("CreateAProfile", "Profile");

                }
                catch (Exception ex)
                {
                    ViewBag.RegisterErrorMsg = ex.Message;
                }

            }
            return View();
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

                    _IloggedInUserModel = await _apiHepler.GetLoggedInUserInfor(results.Access_Token);

                    Session["log"] = results.Access_Token;
                    Token = results.Access_Token;
                    TempData["welcome"] = "Welcome " + Username;
                    Session["Username"] = Username;
                    Session["Token"]=results.Access_Token;

                    //////this will brake my code--(! Illegal assignment of user-roles from aspnetdb, less secured...  )
                    if (_IloggedInUserModel.UserRole.Equals("Customer"))

                    {
                        return RedirectToAction("Index", "Service");

                    }
                    else if (_IloggedInUserModel.UserRole.Equals("ServiceProvider"))
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
    

    public async Task<ActionResult> CreateAProfile(Models.ProfileDisplayModel profile, Models.ProfileDisplayModel.AddressModel address )
        {

            if (ModelState.IsValid)
            {
                //this should be enough to pass to the API endpoint
                profileModel = new HandymanUILibrary.Models.ProfileModel();
                profileModel.Address = new HandymanUILibrary.Models.ProfileModel.AddressModel();
               
                profileModel.Name = profile.Name;
                profileModel.Surname = profile.Surname;
                profileModel.PhoneNumber = profile.PhoneNumber;
                profileModel.DateOfBirth = profile.DateOfBirth;
               
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
                    //Session["profilename"] = loggeuser.Username;
                    //Session["log"] = loggeuser.Id;
                    return RedirectToAction("SignIn","Profile");

                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMsg = ex.Message;
                }

                

            }

            return View();
        }

        public ActionResult AddressDetails()
        {
            return PartialView();
        }
        public async Task<ActionResult> ProfileDetails()
        {
            try
            {
                
                var loggeduser = await _apiHepler.GetLoggedInUserInfor(Token);
                var user = new UserModel();
                user.Id = loggeduser.Id;
                //Getting a profile with its Address
                var results =  await _profileEndPoint.GetProfile(user);

                var tempProfile = new Models.ProfileDisplayModel();
                tempProfile.AddressM = new Models.ProfileDisplayModel.AddressModel();

                tempProfile.Id = results.Id;
                tempProfile.Name = results.Name;
                tempProfile.Surname = results.Surname;
                tempProfile.PhoneNumber = results.PhoneNumber;
                tempProfile.UserId = results.UserId;
                tempProfile.DateOfBirth = results.DateOfBirth;
                
                /*Address population*/
                tempProfile.AddressM.Id = results.Address.Id;
                tempProfile.AddressM.StreetName = results.Address.StreetName;
                tempProfile.AddressM.HouseNumber = results.Address.HouseNumber;
                tempProfile.AddressM.Surburb = results.Address.Surburb;
                tempProfile.AddressM.PostalCode = results.Address.PostalCode;
                tempProfile.AddressM.City = results.Address.City;


                return View(tempProfile);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
            }

            return View();
        }
    }
}