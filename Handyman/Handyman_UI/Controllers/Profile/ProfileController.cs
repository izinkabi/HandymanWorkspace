using Handyman_UI.Models;
using HandymanUILibrary.API;
using HandymanUILibrary.Models;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace Handyman_UI.Controllers
{
    public class ProfileController : Controller
    {

        private IAPIHelper _apiHepler;
        
       
        static private string UserRole;
        
        private HandymanUILibrary.Models.ProfileModel profileModel;      
        private NewUserModel user;

        private IProfileEndPoint _profileEndPoint;
        //private IRegisterEndPoint _registerEndPoint;
       
        //static private string DisplayUserName;
        static private IloggedInUserModel _loggedInUserModel;
        public ProfileController(IAPIHelper aPIHelper,IProfileEndPoint profile,IloggedInUserModel LoggedInUserModel)
        {
            _apiHepler = aPIHelper;
            _profileEndPoint = profile;
            _loggedInUserModel = LoggedInUserModel;
        }

        //Action method for 
        public PartialViewResult CreateAddress()
        {
            
            return PartialView();
        }
            
 
        //Login action function
        

    

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
                    
                   
                    
                    if (Session["Token"] == null)
                    {
                        return RedirectToAction("SignIn", "Login");
                    } else
                        {

                        var token = Session["Token"].ToString();
                        var loggeuser = await _apiHepler.GetLoggedInUserInfor(token);// awaited loggedUser

                        profileModel.UserId = loggeuser.Id;//pass user ID
                        UserRole = loggeuser.UserRole;

                        await _profileEndPoint.PostProfile(profileModel);//waited results of posted user profile

                        if (UserRole == "Customer")
                        {

                            return RedirectToAction("RegisterCustomer", "CustomerHome");

                        }
                        else if (UserRole == "ServiceProvider")
                        {
                            
                            return RedirectToAction("RegisterServiceProvider", "ServiceProviderHome");
                        }
                    }
                    
                     
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

            if (ModelState.IsValid)
            {


                try
                {
                    var token = Session["Token"].ToString();
                     _loggedInUserModel = await _apiHepler.GetLoggedInUserInfor(token);
                    var user = new UserModel();
                    user.Id = _loggedInUserModel.Id;
                    //Getting a profile with its Address
                    var results = await _profileEndPoint.GetProfile(user);

                    var tempProfile = new Models.ProfileDisplayModel();
                    tempProfile.AddressM = new Models.ProfileDisplayModel.AddressModel();

                    tempProfile.ProfileId = results.Id;
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
            }
            return View();
        }


        //The two following methods edit the customer's profile
        // GET: /Profile/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult((int)HttpStatusCode.BadRequest);
            } 
            else
            {
                ProfileDisplayModel profileDisplayModel = new ProfileDisplayModel();
                try
                {
                    var token = Session["Token"].ToString();
                    var loggedUser = await _apiHepler.GetLoggedInUserInfor(token);
                    UserModel user = new UserModel();
                    user.Id = loggedUser.Id;
                    var profile = await _profileEndPoint.GetProfile(user);

                   
                    profileDisplayModel.UserId = profile.UserId;
                    profileDisplayModel.ProfileId = profile.Id;
                    profileDisplayModel.Name = profile.Name;
                    profileDisplayModel.Surname = profile.Surname;
                    profileDisplayModel.DateOfBirth = profile.DateOfBirth;
                    profileDisplayModel.PhoneNumber = profile.PhoneNumber;

                    profileDisplayModel.AddressM = new ProfileDisplayModel.AddressModel();
                    profileDisplayModel.AddressM.Id = profile.Address.Id;
                    profileDisplayModel.AddressM.City = profile.Address.City;
                    profileDisplayModel.AddressM.PostalCode = profile.Address.PostalCode;
                    profileDisplayModel.AddressM.StreetName = profile.Address.StreetName;
                    profileDisplayModel.AddressM.Surburb = profile.Address.Surburb;
                    profileDisplayModel.AddressM.HouseNumber = profile.Address.HouseNumber;
                    if (profile == null)
                    {
                        return HttpNotFound();
                    }
                }catch(Exception ex)
                {
                    throw new Exception(ex.Message);                   
                }
                return View("Edit", profileDisplayModel);
            }
         
            
        }

        // POST: /Profile/Edit/5    

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProfileDisplayModel profile)
        {
            if (ModelState.IsValid)
            {
                var profileUpdate = new ProfileModel();
                try
                {

                    profileUpdate.Address = new ProfileModel.AddressModel();
                    profileUpdate.Id = profile.ProfileId;
                    profileUpdate.Address.City = profile.AddressM.City;
                    profileUpdate.Address.StreetName = profile.AddressM.StreetName;
                    profileUpdate.Address.Surburb = profile.AddressM.Surburb;
                    profileUpdate.Address.PostalCode = profile.AddressM.PostalCode;
                    profileUpdate.Address.HouseNumber = profile.AddressM.HouseNumber;
                    profileUpdate.Address.Id = profile.AddressM.Id;

                    profileUpdate.UserId = profile.UserId;
                    profileUpdate.Name = profile.Name;
                    profileUpdate.PhoneNumber = profile.PhoneNumber;
                    profileUpdate.Surname = profile.Surname;
                    profileUpdate.DateOfBirth = profile.DateOfBirth;
                    TempData["updatedprofile"] = profile.Name + " you edited your Profile";
                    //first update the profile
                    await _profileEndPoint.UpdateProfile(profileUpdate);
                }catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return RedirectToAction("ProfileDetails");
            }
            return View(profile);
        }
    }
}