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
                    
                    //Getting a profile with its Address
                    ProfileModel profile = await _profileEndPoint.GetProfile(_loggedInUserModel.ToString());

                    return View(profile);
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
                ProfileModel profile;
               
                try
                {
                    var token = Session["Token"].ToString();
                    _loggedInUserModel = await _apiHepler.GetLoggedInUserInfor(token);
                    
                     profile = await _profileEndPoint.GetProfile(_loggedInUserModel.Id);

                   
                    //profileDisplayModel.UserId = profile.UserId;
                    //profileDisplayModel.ProfileId = profile.Id;
                    //profileDisplayModel.Name = profile.Name;
                    //profileDisplayModel.Surname = profile.Surname;
                    //profileDisplayModel.DateOfBirth = profile.DateOfBirth;
                    //profileDisplayModel.PhoneNumber = profile.PhoneNumber;

                    //profileDisplayModel.AddressM = new ProfileDisplayModel.AddressModel();
                    //profileDisplayModel.AddressM.Id = profile.Address.Id;
                    //profileDisplayModel.AddressM.City = profile.Address.City;
                    //profileDisplayModel.AddressM.PostalCode = profile.Address.PostalCode;
                    //profileDisplayModel.AddressM.StreetName = profile.Address.StreetName;
                    //profileDisplayModel.AddressM.Surburb = profile.Address.Surburb;
                    //profileDisplayModel.AddressM.HouseNumber = profile.Address.HouseNumber;
                    if (profile == null)
                    {
                        return HttpNotFound();
                    }
                }catch(Exception ex)
                {
                    throw new Exception(ex.Message);                   
                }
                return View("Edit", profile);
            }
         
            
        }

        // POST: /Profile/Edit/5    

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProfileModel profile)
        {
            if (ModelState.IsValid)
            {
               
                try
                {

                    TempData["updatedprofile"] = profile.Name + " you edited your Profile";
                    //first update the profile
                    await _profileEndPoint.UpdateProfile(profile);
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